using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.Persistence
{
    public class ApiContext : DbContext
    {
        //public ApiContext()
        //      : base()
        //{ }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        { }

        public ApiContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        //public DbSet<Product> Products { get; set; }
        //public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //    modelBuilder.Entity<User>()
            //        .HasOne(b => b.Author)
            //        .WithMany(a => a.Books)
            //        .OnDelete(DeleteBehavior.SetNull);
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Types<IObjectWithState>().Configure(c => c.Ignore(p => p.State));
        //}
    }
}
