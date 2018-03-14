using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Model;
using WebApi.Persistence;

namespace WebApi.Tests
{
    [TestClass]
    public class BlogTests : TestBase
    {
        [TestMethod]
        public void ModifyUser()
        {
            using (ApiContext context = dbContextFactory.Create())
            {
                //Blog blog = new Blog
                //{
                //    BlogId= 4,
                //    Rating=0,
                //    Url= "Test URL",
                //    UserId = 4,
                //};

                //Blog blog = new Blog
                //{
                //    BlogId = 4,
                //    Rating = 0,
                //    Url = "Test URL",
                //    User = new User
                //    {
                //        UserId = 3,
                //        Name = "TEst Name"
                //    }
                //};

                //Blog blog = new Blog
                //{
                //    BlogId = 4,
                //    Rating = 0,
                //    Url = "Test URL",
                //    UserId=1,
                //    User = new User// this will be set instead of UserId
                //    {
                //        UserId = 4,
                //        Name = "TEst Name"
                //    }
                //};

                //Blog blog = new Blog
                //{                  
                //    BlogId = 1005,
                //    Rating = 0,
                //    Url = "Test URL",
                //    User = new User
                //    {
                //        Name = "TEst Name"                      
                //    }
                //};

                //Blog blog = new Blog
                //{
                //    Rating = 0,
                //    Url = "Test URL",
                //    UserId = 1,
                //    User = new User// this will be set instead of UserId
                //    {
                //        UserId = 4,
                //        Name = "TEst Name"
                //    }
                //};

                Blog blog = new Blog
                {
                    Url = "Test URL",
                    User = new User// this will be set instead of UserId
                    {
                        Name = "TEst Name"
                    }
                };

                context.Blogs.Update(blog);

                RegisterSaveChanges(context);

                context.SaveChanges();

                blog.Url = "Testing for context";
                RegisterSaveChanges(context);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void ModifyUser_MultipleContextSaveChanges()
        {
            using (ApiContext context = dbContextFactory.Create())
            {
                Blog blog = new Blog
                {
                    Url = "Test URL",
                    User = new User// this will be set instead of UserId
                    {
                        Name = "TEst Name"
                    }
                };

                context.Blogs.Update(blog);

                RegisterSaveChanges(context);

                context.SaveChanges();

                blog.Url = "Testing for context";
                RegisterSaveChanges(context);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void DeleteBlog()
        {
            using (ApiContext context = dbContextFactory.Create())
            {
                Blog blog = new Blog
                {
                    BlogId = 1015,
                    Rating = 0,
                    Url = "string",
                    //UserId = 4,
                    User = new User
                    {
                        UserId = 4,
                        Name = "TEst Name"
                    }
                };

                context.Blogs.Remove(blog);

                RegisterSaveChanges(context);

                context.SaveChanges();
            }
        }
    }
}
