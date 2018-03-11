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

        public DbSet<User> Users { get; set; }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        //public DbSet<Product> Products { get; set; }
        //public DbSet<Address> Address { get; set; }
        
        
    }
}
