using EventsModule.Core;
using MediatR;
using OneOf.Types;
using OneOf;

namespace EventsModule.Logic.Wrapper
{
    public class DeleteResponse<T> : IRequest<OneOf<Task, NotFound, Problem>> where T : class
    {
        public T? Item { get; set; }
        public string Error { get; set; } = "";
        public bool Success { get; set; } = true;

        public DeleteResponse()
        {

        }
        public DeleteResponse(T? item)
        {
            Item = item;
        }
        public DeleteResponse(string error, T item)
        {
            Error = error;
            Item = item;
        }
    }
}
