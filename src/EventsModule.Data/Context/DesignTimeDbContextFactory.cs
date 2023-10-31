using EventsModule.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Church.Management.Data.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EventsModuleMySQLContext>
    {
        public EventsModuleMySQLContext CreateDbContext(string[] args)
        {
            var path = Directory.GetCurrentDirectory();
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .Build();

            var mysqlBuilder = new DbContextOptionsBuilder<EventsModuleMySQLContext>();
            var connectionString = configuration.GetConnectionString("MySQLConnectionStrings");            
            mysqlBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            mysqlBuilder.EnableSensitiveDataLogging(true);

            return new EventsModuleMySQLContext(mysqlBuilder.Options);
        }
    }
}
