using AutoMapper;
using EventsModule.Core;
using EventsModule.Data.Database.Interface;
using EventsModule.Data.Models;
using EventsModule.Logic.Response;
using EventsModule.Logic.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace EventsModule.Logic.EventHandlers
{
    public partial class EventListHandler : IRequestHandler<ListRequest, OneOf<List<EventResponse>, Problem>>
    {
        private IMapper _mapper;
        private IRepository<Event> _repository;

        public EventListHandler(IMapper mapper, IRepository<Event> repository)
            => (_repository, _mapper) = (repository, mapper);

        public async Task<OneOf<List<EventResponse>, Problem>> Handle(ListRequest request, CancellationToken token)
        {            
            try
            {
                int skip = (request.StartPage * request.PageSize) - request.PageSize;
                var result = await _repository.GetSet()
                    .OrderBy(x => x.ID)
                    .Where(x => x.ID < skip)
                    .Take(request.PageSize)
                    .ToListAsync(token);

                var pagedData = _mapper.Map<List<EventResponse>>(result);
                return pagedData;
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
