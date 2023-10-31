using EventsModule.Core;
using EventsModule.Logic.Response;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EventsModule.Logic.Wrapper
{
    public class ListRequest : IRequest<OneOf<List<EventResponse>, Problem>>
    {
        public int StartPage { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
