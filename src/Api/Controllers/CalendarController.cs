using System.Net;
using System.Net.Mime;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shedy.Api.Requests;
using Shedy.Api.Responses;
using Shedy.Core.Commands.CreateCalendar;
using Shedy.Core.Queries.GetCalendar;

namespace Shedy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CalendarController : ControllerBase
{
    private readonly IMediator _mediator;

    public CalendarController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCalendarRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateCalendar>();
        var result = await _mediator.Send(command, cancellationToken);
        var response = result.Adapt<CreateCalendarResponse>();

        var routeValues = new { calendarId = response.Calendar.Id };
        return CreatedAtAction("Get", routeValues, response.Calendar);
    }

    [HttpGet("{calendarId:guid}")]
    [ProducesResponseType(typeof(GetCalendarResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> GetAsync(Guid calendarId, CancellationToken cancellationToken)
    {
        var query = new GetCalendar(calendarId);
        var result = await _mediator.Send(query, cancellationToken);
        var response = result.Adapt<GetCalendarResponse>();
        return Ok(response);
    }
}