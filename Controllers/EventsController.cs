using AutoMapper;
using EventsModule.Data.Models;
using EventsModule.Logic.Request;
using EventsModule.Logic.Response;
using EventsModule.Logic.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static IdentityServer4.Models.IdentityResources;

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
        private readonly HttpClient _client;

        public EventsController(IMediator mediator, IMapper mapper, UserManager<User> userManager, HttpClient client)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userManager = userManager;
            _client = client;
            _client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
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

            return CreatedAtAction(nameof(Get), new { id = response.ID }, response);
        }

        // Get with ID API
        [HttpGet("{ID}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SingleEventResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int ID, CancellationToken token)
        {
            var req = new GetRequest<EventResponse>() { ID = ID };

            // Implements CQRS Pattern
            var response = await _mediator.Send(req, token);
            if (response != null)
            {
                var jsonUser = await _client.GetFromJsonAsync<UserJson>("/users/1");

                var res = new SingleEventResponse
                {
                    CreatedBy = response.CreatedBy,
                    CreatedByID = response.CreatedByID,
                    Description = response.Description,
                    EndTime = response.EndTime,
                    StartTime = response.StartTime,
                    ID = response.ID,
                    Name = response.Name,
                    Title = response.Title,
                    UserJson = jsonUser
                };
                return Ok(res);
            }
            return NotFound(req);
        }

        // Get All API
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<EventResponse>))]
        public async Task<ActionResult<PagedResponse<EventResponse>>> GetAll(ListRequest request, CancellationToken token)
        {
            // Implements CQRS Pattern
            var response = await _mediator.Send(request, token);
            return Ok(response);
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

            if (response == null)
                return NotFound(request);
            else
            {
                return NoContent();
            }
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
            if (response.Success)
            {
                return Ok();
            }
            return NotFound(req);
        }
    }
}
