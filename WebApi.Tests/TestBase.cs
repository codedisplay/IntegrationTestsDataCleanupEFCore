using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebApi.Persistence;

namespace WebApi.Tests
{
    public class TestBase
    {
        protected DbContextFactory dbContextFactory { get; private set; }

        protected List<CleanUpDataInfo> CleanUpDataInfoes { get; set; } = new List<CleanUpDataInfo>();

        private int SaveChangesAttempts;

        [TestInitialize]
        public void Initialize()
        {
            dbContextFactory = new DbContextFactory();
            SaveChangesAttempts = 0;
        }

        protected void RegisterSaveChanges(ApiContext context)
        {
            SaveChangesAttempts++;

            List<EntityEntry> changes = context.ChangeTracker.Entries().ToList();

            foreach (EntityEntry change in changes)
            {
                EntityState state = change.State;
                object updatedEntityEntry = change.Entity;
                PropertyValues dbPropertyValues =
                    state != EntityState.Added
                        ? dbPropertyValues =
                            context.Entry(updatedEntityEntry).GetDatabaseValues()
                        : null;

                CleanUpDataInfoes.Add(
                    new CleanUpDataInfo
                    {
                        EntityEntry = change,
                        Entity = updatedEntityEntry,
                        Values = dbPropertyValues,
                        EntityState = state,
                        SaveChangesAttempt = SaveChangesAttempts,
                        DateTime = DateTime.Now
                    });

                PrintValues(dbPropertyValues);
                PrintValues(change.CurrentValues);
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            // All the data needs to be reverted back in the reverse order 
            // of the order in which they are modified(i.e, SaveChangesAttempt) into DB
            // For the values modified in an instance, they should be undoed 
            // as in the same order in which they are added in Context ChangeTracker
            foreach (var cleanUpDataInfo in CleanUpDataInfoes
                .OrderBy(cudi => cudi.DateTime)//TODO: remove this condition?
                .OrderByDescending(cudi => cudi.SaveChangesAttempt))
            {
                using (ApiContext context = dbContextFactory.Create())
                {
                    switch (cleanUpDataInfo.EntityState)
                    {
                        case EntityState.Modified:
                            context.Attach(cleanUpDataInfo.Entity);
                            context.Entry(cleanUpDataInfo.Entity).CurrentValues.SetValues(cleanUpDataInfo.Values);
                            break;
                        case EntityState.Added:
                            context.Remove(cleanUpDataInfo.Entity);
                            break;
                        case EntityState.Deleted:
                            string databaseName = context.Database.GetDbConnection().Database;
                            var mapping = context.Model.FindEntityType(cleanUpDataInfo.Entity.GetType()).Relational();
                            string schema = mapping.Schema ?? "dbo";
                            string tableName = mapping.TableName;
                            string rawSqlIdentityInsertOn = $"SET IDENTITY_INSERT [{databaseName}].[{schema}].[{tableName}] ON";
                            string rawSqlIdentityInsertOff = $"SET IDENTITY_INSERT [{databaseName}].[{schema}].[{tableName}] OFF";

                            // Data Fixup 
                            var references = cleanUpDataInfo.EntityEntry.References;
                            foreach (ReferenceEntry referenceEntry in references)
                            {
                                // Set this to null, as a workaround for the scenario,
                                // when Navigation Property(User) value is passed along
                                // with it's corresponding field(UserId) value
                                referenceEntry.CurrentValue = null;
                            }

                            using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                            {
                                //context.Database.ExecuteSqlCommand($"SET IDENTITY_INSERT [WebApiDatabase].[dbo].[Users] ON");
                                // Commented were not working
                                //context.Database.ExecuteSqlCommand($"SET IDENTITY_INSERT [{databaseName}].[{schema}].[{tableName}] ON");
                                //context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [{0}].[{1}].[{2}] ON", databaseName, schema, tableName);
                                context.Database.ExecuteSqlCommand(rawSqlIdentityInsertOn);

                                context.Add(cleanUpDataInfo.Entity);

                                context.SaveChanges();

                                context.Database.ExecuteSqlCommand(rawSqlIdentityInsertOff);

                                transaction.Commit();

                                continue;
                            }
                        default:
                            continue;
                            //break;
                    }

                    context.SaveChanges();
                }
            }
        }

        public static void PrintValues(PropertyValues values)
        {
            if (values == null)
                return;

            foreach (var propertyName in values.Properties)
            {
                Debug.WriteLine("Property {0} has value: '{1}'",
                                  propertyName, values[propertyName]);
            }

            Debug.WriteLine("-------------------------");
        }
    }
}