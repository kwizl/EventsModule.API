using Serilog;
using Serilog.Exceptions;

namespace EventsModule.API.ConfigureServices
{
    public static class ConfigureSerilogExtension
    {
        public static void ConfigureSerilog(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((_, configuration) =>
            {
                configuration.Enrich.FromLogContext();
                configuration.Enrich.WithMachineName();
                configuration.Enrich.WithExceptionDetails();
                configuration.Enrich.WithProperty("ApplicationName", "Events Module"!);
            });
        }
    }
}
