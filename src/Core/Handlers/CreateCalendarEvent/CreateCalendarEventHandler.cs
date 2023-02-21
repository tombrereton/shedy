using Ardalis.GuardClauses;
using MediatR;
using Shedy.Core.Interfaces;

namespace Shedy.Core.Handlers.CreateCalendarEvent;

public class CreateCalendarEventHandler : IRequestHandler<CreateCalendarEvent, CreateCalendarEventResult>
{
    private readonly ICalendarRepository _repository;

    public CreateCalendarEventHandler(ICalendarRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateCalendarEventResult> Handle(CreateCalendarEvent request, CancellationToken cancellationToken)
    {
        var calendar = await _repository.GetAsync(request.CalendarId, cancellationToken);
        Guard.Against.Null(calendar, nameof(calendar));
        
        calendar.AddEvent(request.Event);
        await _repository.SaveAsync(calendar, cancellationToken);
        
        return new CreateCalendarEventResult(request.CalendarId, request.Event);
    }
}