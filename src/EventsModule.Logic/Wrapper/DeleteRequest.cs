using EventsModule.Core;
using MediatR;
using OneOf.Types;
using OneOf;

namespace EventsModule.Logic.Wrapper
{
    public class DeleteRequest<T> : IRequest<OneOf<Task, NotFound, Problem>> where T : class
    {
        public int ID { get; set; }
    }
}
