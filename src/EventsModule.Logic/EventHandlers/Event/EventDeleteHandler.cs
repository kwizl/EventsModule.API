using AutoMapper;
using EventsModule.Core;
using EventsModule.Data.Database.Interface;
using EventsModule.Data.Models;
using EventsModule.Logic.Response;
using EventsModule.Logic.Wrapper;
using Humanizer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace EventsModule.Logic.EventHandlers
{
    public partial class EventDeleteHandler : IRequestHandler<DeleteRequest<EventResponse>, OneOf<Task, NotFound, Problem>>
    {
        private IMapper _mapper;
        private IUnitOfWork _uow;
        private ILogger<Event> _logger;
        private IRepository<Event> _repository;

        public EventDeleteHandler(IMapper mapper, IRepository<Event> repository, IUnitOfWork uow, ILogger<Event> logger)
            => (_repository, _mapper, _uow, _logger) = (repository, mapper, uow, logger);

        public async Task<OneOf<Task, NotFound, Problem>> Handle(DeleteRequest<EventResponse> request, CancellationToken token)
        {
            try
            {
                // Get the item from db
                var result = await _repository.GetByID(request.ID).SingleOrDefaultAsync()!;

                if (result is null)
                    throw new InvalidOperationException($"The {nameof(Event).Humanize(LetterCasing.Title)} entity does not exist");

                _uow.BeginTransaction();
                _repository.Delete(result);

                await _uow.SaveChangesAsync(token);
                await _uow.CommitAsync(token);

                return Task.CompletedTask;
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
