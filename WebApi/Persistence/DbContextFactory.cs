using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Persistence
{
    public class DbContextFactory 
        //: IDbContextFactory
    {
        //public ApiContext Create()
        //{
        //    var environmentName = Environment.GetEnvironmentVariable("Hosting:Environment");
        //    var basePath = AppContext.BaseDirectory;
        //    return Create(basePath, environmentName);
        //}

        //public ApiContext Create(DbContextFactoryOptions options)
        //{
        //    return Create(Directory.GetCurrentDirectory(), options.EnvironmentName);
        //}

        //private ApiContext Create(string basePath, string environmentName)
        //{
        //    var builder = new ConfigurationBuilder()
        //    .SetBasePath(basePath)
        //    .AddJsonFile("appsettings.json")
        //    .AddJsonFile($"appsettings.{environmentName}.json", true)
        //    .AddEnvironmentVariables();

        //    var config = builder.Build();
        //    var connstr = config.GetConnectionString("DefaultConnection");

        //    if (string.IsNullOrWhiteSpace(connstr) == true)
        //    {
        //        throw new InvalidOperationException(
        //        "Could not find a connection string named '(DefaultConnection)'.");
        //    }
        //    else
        //    {
        //        return Create(connstr);
        //    }
        //}

        public ApiContext Create(string connectionString =
            "Server=LENOVO-PC\\SQLEXPRESS;Database=WebApiDatabase;Trusted_Connection=True;")
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(
                $"{nameof(connectionString)} is null or empty.",
                nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(connectionString);
            return new ApiContext(optionsBuilder.Options);
        }
    }
}
