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
                options.UseMySql(builder.Configuration.GetConnectionString("MySQLConnectionString"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySQLConnectionString")));
                options.EnableSensitiveDataLogging(true);
            });
        }
    }
}
