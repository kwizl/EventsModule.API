using EventsModule.Data.Context;
using EventsModule.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace EventsModule.API.ConfigureServices
{
    public static class ConfigureIdentityExtension
    {
        public static void ConfigureIdentity(this WebApplicationBuilder builder)
        {
            // IdentityServer Users and Roles
            builder.Services.AddIdentity<User, IdentityRole>(option =>
            {
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 8;
                option.Password.RequireDigit = false;
            })
            .AddSignInManager<SignInManager<User>>()
            .AddEntityFrameworkStores<EventsModuleMySQLContext>();
        }
    }
}
