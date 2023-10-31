using EventsModule.Core;
using EventsModule.Logic.Response;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EventsModule.Logic.Wrapper
{
    public class GetRequest<T> : IRequest<OneOf<EventResponse, NotFound, Problem>> where T : class
    {
        public int ID { get; set; }
    }
}
