using EventsModule.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EventsModule.API.Extensions
{
    public static class HostExtension
    {
        // Connects to MySQL Server
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0) where TContext : EventsModuleMySQLContext
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // Returns null if the service is not found
                var context = services.GetRequiredService<TContext>();

                try
                {
                    InvokeSeeder(context);
                }
                catch (Exception ex)
                {
                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                }
            }
            return host;
        }

        // Migrates and Seeds data to the database
        private static void InvokeSeeder<TContext>(TContext context) where TContext : EventsModuleMySQLContext
        {
            context.Database.Migrate();
        }
    }
}
