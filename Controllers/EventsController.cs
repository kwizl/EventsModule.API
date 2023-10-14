using AutoMapper;
using EventsModule.Data.Models;
using EventsModule.Logic.Request;
using EventsModule.Logic.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace EventsModule.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("ap1/[controller]")]
    public class EventsController : ControllerBase
    {
        private IMapper _mapper;
        private IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public EventsController(IMediator mediator, IMapper mapper, UserManager<User> userManager)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EventResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(EventCreateRequest request, CancellationToken token)
        {
            // Implements CQRS Pattern
            var response = await _mediator.Send(request, token);

            return CreatedAtAction(nameof(Get), new { id = response.ID }, response);
        }

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
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound(req);
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<EventResponse>))]
        public async Task<ActionResult<PagedResponse<EventResponse>>> GetAll([FromQuery] PaginationFilter filter, CancellationToken token)
        {
            var req = new ListRequest<EventResponse>() { filter = filter };

            // Implements CQRS Pattern
            var response = await _mediator.Send(req, token);
            return Ok(response);
        }

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

            if (response == null)
                return NotFound(request);
            else
            {
                return NoContent();
            }
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(EmptyResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(EventResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int ID, CancellationToken token)
        {
            // Implements CQRS Pattern
            var response = await _mediator.Send(ID, token);

            if (response == null)
                return NotFound(ID);
            else
            {   
                return NoContent();
            }
        }
    }
}
