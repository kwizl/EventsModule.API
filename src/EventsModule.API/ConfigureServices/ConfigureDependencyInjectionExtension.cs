using EventsModule.Data.Context.Service;
using EventsModule.Data.Database.Interface;
using EventsModule.Data.Database.Service;

namespace EventsModule.API.ConfigureServices
{
    public static class ConfigureDependencyInjectionExtension
    {
        public static void ConfigureDependencyInjection(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
