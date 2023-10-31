using Microsoft.IdentityModel.Tokens;

namespace EventsModule.API.ConfigureServices
{
    public static class ConfigureIdentityServerExtension
    {
        // IdentityServer4 Authentication
        public static void ConfigureIdentityServer(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:7192";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "eventsModuleClient"));
            });
        }
    }
}
