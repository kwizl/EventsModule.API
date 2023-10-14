using EventsModule.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EventsModule.API.ConfigureServices
{
    public static class ConfigureAuthenticationExtension
    {
        public static void ConfigureAuthentication(this WebApplicationBuilder builder)
        {
            var key = GetToken.Key();

            // Authentication with JWT Token
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidIssuer = builder.Configuration["Authentication:Schemes:Bearer:ValidIssuer"],
                        ValidAudience = builder.Configuration["Authentication:Schemes:Bearer:ValidAudiences"],
                    };
                });
        }
    }
}
