using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shedy.Core.Handlers.CreateCalendar;

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
    public async Task<IActionResult> CreateCalendarAsync(CancellationToken cancellationToken)
    {
        var command = new CreateCalendar(Guid.Empty);
        var result = await _mediator.Send(command, cancellationToken);
        return new OkObjectResult(result);
    }
}