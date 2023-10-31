using AutoMapper;
using EntityFramework.Exceptions.Common;
using EventsModule.Core;
using EventsModule.Data.Database.Interface;
using EventsModule.Data.Models;
using EventsModule.Logic.Request;
using EventsModule.Logic.Response;
using Humanizer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace EventsModule.Logic.EventHandlers
{
    public partial class EventUpdateHandler : IRequestHandler<EventUpdateRequest, OneOf<Task, NotFound, Problem>>
    {
        private IMapper _mapper;
        private IUnitOfWork _uow;
        private ILogger<Event> _logger;
        private IRepository<Event> _repository;

        public EventUpdateHandler(IMapper mapper, IRepository<Event> repository, IUnitOfWork uow, ILogger<Event> logger)
           => (_repository, _mapper, _uow, _logger) = (repository, mapper, uow, logger);

        public async Task<OneOf<Task, NotFound, Problem>> Handle(EventUpdateRequest request, CancellationToken token)
        {
            try
            {
                // Get the item from db
                var response = await _repository.GetByID(request.ID).SingleOrDefaultAsync();

                if (response is null)
                    throw new InvalidOperationException($"The {nameof(Event).Humanize(LetterCasing.Title)} entity does not exist");

                // Map the request to response
                _mapper.Map(request, response);
                _uow.BeginTransaction();

                await _repository.Update(response);
                await _uow.SaveChangesAsync(token);
                await _uow.CommitAsync(token);

                return Task.CompletedTask;
            }
            catch (UniqueConstraintException ex)
            {
                _logger.LogError(ex, "Could not insert one or more of the {Entity} - duplicate name exists",
                    nameof(Event).Humanize(LetterCasing.Title));
                return new Problem
                {
                    ErrorCode = 500,
                    Description =
                        $"Could not insert duplicate {nameof(Event).Humanize(LetterCasing.Title)} - name exist - {ex.Message}",
                    Exception = ex
                };
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Could not save updates to {Name} has changed since update began, please reload then try again",
                    request.Name);
                return new Problem
                {
                    ErrorCode = 500,
                    Description = $"Could not save updates to {request.Name} has changed since update began, please reload then try again",
                    Exception = ex,
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
