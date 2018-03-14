using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Model;
using WebApi.Persistence;

namespace WebApi.Tests
{
    [TestClass]
    public class PostsTests : TestBase
    {
        [TestMethod]
        public void ModifyPost()
        {
            ApiContext context = dbContextFactory.Create("Server=LENOVO-PC\\SQLEXPRESS;Database=WebApiDatabase;Trusted_Connection=True;");

            Post p = new Post
            {
                PostId = 7,
                Title = "fffff",
                BlogId = 2,
                Content = "eeeee"
            };

            context.Posts.Update(p);

            List<EntityEntry> changes = context.ChangeTracker.Entries().ToList();

            foreach (EntityEntry change in changes)
            {
                EntityState state = change.State;
                object updatedEntityEntry = change.Entity;
                PropertyValues dbPropertyValues=null;
                
                if(state!= EntityState.Added)
                dbPropertyValues= context.Entry(updatedEntityEntry).GetDatabaseValues();

                CleanUpDataInfoes.Add(
                    new CleanUpDataInfo
                    {
                        Entity = updatedEntityEntry,
                        Values = dbPropertyValues,
                        EntityState = state,
                        DateTime = DateTime.Now
                    });

                PrintValues(dbPropertyValues);
                PrintValues(change.CurrentValues);
            }

            context.SaveChanges();

            context.Dispose();
        }

        //[TestMethod]
        //public async Task PostTestMethod()
        //{
        //    PostsController postsController = new PostsController(dbContextFactory.Create("Server=LENOVO-PC\\SQLEXPRESS;Database=WebApiDatabase;Trusted_Connection=True;"));

        //    Post p = new Post
        //    {
        //        PostId = 7,
        //        Title = "UnitTEst",
        //        BlogId = 4,
        //        Content = "sdf"
        //    };

        //    await postsController.PutPost(p.PostId, p);



        //}

        
    }
}
