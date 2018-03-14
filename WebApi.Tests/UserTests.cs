using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Model;
using WebApi.Persistence;

namespace WebApi.Tests
{
    [TestClass]
    public class UserTests : TestBase
    {
        [TestMethod]
        public void ModifyUser()
        {
            using (ApiContext context = dbContextFactory.Create())
            {
                User user = new User
                {
                    UserId = 1,
                    Name = "Unit Test User"
                };

                context.Users.Update(user);

                RegisterSaveChanges(context);

                context.SaveChanges();
            }
        }

        [TestMethod]
        public void AddUser()
        {
            using (ApiContext context = dbContextFactory.Create())
            {
                User user = new User
                {
                    Name = "Unit Test User"
                };

                context.Users.Add(user);

                RegisterSaveChanges(context);

                context.SaveChanges();
            }
        }

        [TestMethod]
        public void DeleteUser()
        {
            using (ApiContext context = dbContextFactory.Create())
            {
                User user = new User
                {
                    UserId = 1013,
                    Name = "string"
                };

                context.Users.Remove(user);

                RegisterSaveChanges(context);

                context.SaveChanges();
            }
        }
    }
}
