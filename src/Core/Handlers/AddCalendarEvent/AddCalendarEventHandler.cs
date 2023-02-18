using Ardalis.GuardClauses;
using MediatR;
using Shedy.Core.Builders;
using Shedy.Core.Interfaces;

namespace Shedy.Core.Handlers.AddCalendarEvent;

public class AddCalendarEventHandler : IRequestHandler<AddCalendarEvent, AddCalendarEventResult>
{
    private readonly ICalendarRepository _repository;

    public AddCalendarEventHandler(ICalendarRepository repository)
    {
        _repository = repository;
    }

    public async Task<AddCalendarEventResult> Handle(AddCalendarEvent request, CancellationToken cancellationToken)
    {
        var calendar = await _repository.GetAsync(request.CalendarId, cancellationToken);
        Guard.Against.Null(calendar, nameof(calendar));
        
        calendar.AddEvent(request.Event);
        await _repository.SaveAsync(calendar, cancellationToken);
        
        return new AddCalendarEventResult(request.CalendarId, request.Event);
    }
}