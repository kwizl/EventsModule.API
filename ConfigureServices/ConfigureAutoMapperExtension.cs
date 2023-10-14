using EventsModule.Logic.AutoMapperProfiles;

namespace EventsModule.API.ConfigureServices
{
    public static class ConfigureAutoMapperExtension
    {
        public static void ConfigureAutoMapper(this WebApplicationBuilder builder)
        {
            // Injects Automapper service
            builder.Services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
        }
    }
}
