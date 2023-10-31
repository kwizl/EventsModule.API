using AutoMapper;
using EventsModule.Core;
using EventsModule.Data.Database.Interface;
using EventsModule.Data.Models;
using EventsModule.Logic.Response;
using EventsModule.Logic.Wrapper;
using Humanizer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace EventsModule.Logic.EventHandlers
{
    public partial class EventGetHandler : IRequestHandler<GetRequest<EventResponse>, OneOf<EventResponse, NotFound, Problem>>
    {
        private IMapper _mapper;
        private IRepository<Event> _repository;

        public EventGetHandler(IMapper mapper, IRepository<Event> repository)
            => (_repository, _mapper) = (repository, mapper);

        public async Task<OneOf<EventResponse, NotFound, Problem>> Handle(GetRequest<EventResponse> request, CancellationToken token)
        {
            try
            {
                Event? result = new();

                if (result is not null)
                {
                    result = await _repository.GetSet().SingleOrDefaultAsync(x => x.ID == request.ID, token)!;

                    var res = _mapper.Map<EventResponse>(result);
                    return res;
                }

                return new NotFound();
            }
            catch (Exception ex)
            {
                return new Problem
                {
                    ErrorCode = 500,
                    Description = ex.Message,
                    Exception = ex
                };
            }
        }
    }
}
