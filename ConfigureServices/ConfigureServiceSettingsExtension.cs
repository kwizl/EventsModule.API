using EventsModule.API.Settings;

namespace EventsModule.API.ConfigureServices
{
    public static class ConfigureServiceSettingsExtension
    {
        public static void ConfigureServiceSettings(this WebApplicationBuilder builder)
        {
            builder.Services.AddOptions<ElasticSearchSettings>()
                .Bind(builder.Configuration.GetRequiredSection(nameof(ElasticSearchSettings)))
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }
    }
}
