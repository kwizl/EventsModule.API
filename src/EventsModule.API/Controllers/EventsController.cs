using AutoMapper;
using EventsModule.Logic.Request;
using EventsModule.Logic.Response;
using EventsModule.Logic.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace EventsModule.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("ClientIdPolicy")]
    public class EventsController : ControllerBase
    {
        private IMapper _mapper;
        private IMediator _mediator;

        public EventsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // Create API
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EventResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(EventCreateRequest request, CancellationToken token)
        {
            // Implements CQRS Pattern
            var response = await _mediator.Send(request, token);

            return response.Match(
                // success - dto
                dto => CreatedAtAction(nameof(Get), new { dto.ID }, dto),
                // error
                problem => Problem(problem.Description)
            );
        }

        // Get with ID API
        [HttpGet("{ID}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int ID, CancellationToken token)
        {
            var req = new GetRequest<EventResponse>() { ID = ID };

            // Implements CQRS Pattern
            var response = await _mediator.Send(req, token);
            return response.Match(
                Ok,
                _ => (ActionResult)NotFound(),
                problem => Problem(problem.Description)
            );            
        }

        // Get All API
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EventResponse>))]
        public async Task<ActionResult<List<EventResponse>>> GetAll(ListRequest request, CancellationToken token)
        {
            // Implements CQRS Pattern
            var response = await _mediator.Send(request, token);
            return response.Match(
                Ok,
                problem => Problem(problem.Description)
            );
        }

        // Update API
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(EmptyResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(EventResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(EventUpdateRequest request, CancellationToken token)
        {
            // Implements CQRS Pattern
            var response = await _mediator.Send(request, token);

            return response.Match(
                _ => (ActionResult)NoContent(),
                _ => NotFound(),
                (problem) => Problem(problem.Description)
            );
        }

        // Delete API
        [HttpDelete("{ID}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmptyResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DeleteRequest<EventResponse>))]
        public async Task<IActionResult> Delete(int ID, CancellationToken token)
        {
            // Implement Wrapper for handling delete response
            var req = new DeleteRequest<EventResponse>() { ID = ID };

            // Implement CQRS for Delete
            var response = await _mediator.Send(req, token);

            return response.Match(
                // Success
                _ => (ActionResult)Ok(),
                // Not found
                _ => NotFound(),
                // Problem
                problem => Problem(problem.Description));
        }
    }
}
