using EventsModule.API.Interfaces;
using EventsModule.API.Services;
using IdentityServer4.Services;

namespace EventsModule.API.ConfigureServices
{
    public static class ConfigureBearerTokenExtension
    {
        public static void ConfigureBearerToken(this WebApplicationBuilder builder)
        {
            // Registering service for token
            builder.Services.AddScoped<IBearerToken, BearerToken>();
        }
    }
}
