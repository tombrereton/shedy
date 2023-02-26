using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shedy.Api.Requests;
using Shedy.Core.Commands.CreateCalendar;

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
        var command = new CreateCalendar(request.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok();

    }
}