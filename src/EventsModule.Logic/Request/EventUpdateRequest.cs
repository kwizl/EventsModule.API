using EventsModule.Core;
using EventsModule.Logic.Response;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EventsModule.Logic.Request
{
    public sealed partial class EventUpdateRequest : IRequest<OneOf<Task, NotFound, Problem>>
    {
        public int ID { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }

        public string Name { get; set; } = null!;

        public int UserID { get; set; }
    }
}
