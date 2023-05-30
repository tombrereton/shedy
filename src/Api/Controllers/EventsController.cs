// using System.Net;
// using System.Net.Mime;
// using Mapster;
// using MediatR;
// using Microsoft.AspNetCore.Mvc;
// using Shedy.Api.Requests;
// using Shedy.Api.Responses;
// using Shedy.Domain.Commands.CreateCalendar;
// using Shedy.Domain.Queries.GetCalendar;
//
// namespace Shedy.Api.Controllers;
//
// [ApiController]
// [Route("api/[controller]")]
// public class EventsController : ControllerBase
// {
//     private readonly IMediator _mediator;
//
//     public EventsController(IMediator mediator)
//     {
//         _mediator = mediator;
//     }
//
//
//     [HttpGet("{eventId:guid}")]
//     [ProducesResponseType(typeof(GetCalendarResponse), (int)HttpStatusCode.OK)]
//     [ProducesResponseType((int)HttpStatusCode.NotFound)]
//     [Produces(MediaTypeNames.Application.Json)]
//     public async Task<IActionResult> GetAsync(Guid eventId, CancellationToken cancellationToken)
//     {
//         var query = new GetCalendar(eventId);
//         var result = await _mediator.Send(query, cancellationToken);
//         var response = result.Adapt<GetCalendarResponse>();
//         return Ok(response);
//     }
// }