using EventsModule.Core;
using EventsModule.Logic.Response;
using MediatR;
using OneOf;

namespace EventsModule.Logic.Request
{
    public sealed partial class EventCreateRequest : IRequest<OneOf<EventResponse, Problem>>
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Name { get; set; } = null!;
    }
}
