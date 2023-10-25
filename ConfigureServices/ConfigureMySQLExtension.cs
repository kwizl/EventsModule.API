using EventsModule.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EventsModule.API.ConfigureServices
{
    public static class ConfigureMySQLExtension
    {
        public static void ConfigureMySQL(this WebApplicationBuilder builder)
        {
            // MySQL Configuration
            builder.Services.AddDbContext<EventsModuleMySQLContext>(options =>
            {
                options.UseMySql(builder.Configuration.GetConnectionString("MySQLConnectionStrings"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySQLConnectionStrings")));
                options.EnableSensitiveDataLogging(true);
            });
        }
    }
}
