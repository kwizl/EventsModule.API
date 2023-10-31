using EventsModule.API.Settings;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Diagnostics;
using System.Reflection;

namespace EventsModule.API.ConfigureServices
{
    public static class ConfigureSerilogExtension
    {
        public static void ConfigureLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, configuration) =>
            {

                // Get elastic search settings
                var elasticSearchSettings = new ElasticSearchSettings();
                builder.Configuration.GetRequiredSection(nameof(ElasticSearchSettings)).Bind(elasticSearchSettings);

                var applicationName = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()!.Location).ProductName;
                var applicationVersion = Assembly.GetExecutingAssembly().GetName().Version;

                configuration.WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(new Uri(elasticSearchSettings.ConnectionString))
                    {
                        IndexFormat = $"{applicationName}-{{0:yyyy-MMM-dd}}-0",
                        AutoRegisterTemplate = true,
                        NumberOfShards = 2,
                        NumberOfReplicas = 2
                    }
                );

                configuration.Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithMachineName()
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .Enrich.WithProperty("ApplicationName", applicationName!)
                    .Enrich.WithProperty("Version", applicationVersion!)
                    .WriteTo.Console();
            });
        }
    }
}
