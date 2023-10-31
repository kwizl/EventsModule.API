using EventsModule.Logic.EventHandlers;
using MediatR;

namespace EventsModule.API.ConfigureServices
{
    public static class ConfigureMediatRExtension
    {
        public static void ConfigureMediatR(this WebApplicationBuilder builder)
        {
            // Configure CQRS Service
            builder.Services.AddMediatR(typeof(EventCreateHandler).Assembly);
        }
    }
}
