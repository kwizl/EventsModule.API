using AutoMapper;
using EntityFramework.Exceptions.Common;
using EventsModule.Core;
using EventsModule.Data.Database.Interface;
using EventsModule.Data.Models;
using EventsModule.Logic.Request;
using EventsModule.Logic.Response;
using Humanizer;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;

namespace EventsModule.Logic.EventHandlers
{
    public partial class EventCreateHandler : IRequestHandler<EventCreateRequest, OneOf<EventResponse, Problem>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Event> _repository;
        private readonly ILogger<EventCreateHandler> _logger;

        public EventCreateHandler(IMapper mapper, IRepository<Event> repository, IUnitOfWork uow, ILogger<EventCreateHandler> logger)
            => (_repository, _mapper, _uow, _logger) = (repository, mapper, uow, logger);

        public async Task<OneOf<EventResponse, Problem>> Handle(EventCreateRequest request, CancellationToken token)
        {
            try
            {
                var req = _mapper.Map<Event>(request);
                req.DateCreated = DateTime.UtcNow.AddHours(-3);
                req.DateModified = DateTime.UtcNow.AddHours(-3);
                req.UserID = 1;

                _uow.BeginTransaction();

                await _repository.Add(req);
                await _uow.CommitAsync(token);

                return _mapper.Map<EventResponse>(req);
            }
            catch (UniqueConstraintException ex)
            {
                _logger.LogError(ex, "Could not insert one or more of the {Entity} - duplicate name exists",
                    nameof(Event).Humanize(LetterCasing.Title));
                return new Problem
                {
                    ErrorCode = 500,
                    Description =
                        $"Could not insert duplicate {nameof(Event).Humanize(LetterCasing.Title)} - name exist - { ex.Message }",
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting one or more of the {Entity}",
                    nameof(Event).Humanize(LetterCasing.Title));
                return new Problem
                {
                    ErrorCode = 500,
                    Description =
                        $"Could not insert one of more of the {nameof(Event).Humanize(LetterCasing.Title)} {ex.Message}",
                    Exception = ex
                };
            }
        }
    }
}
