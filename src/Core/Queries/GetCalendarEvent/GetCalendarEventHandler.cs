using Ardalis.GuardClauses;
using MediatR;
using Shedy.Core.Interfaces;

namespace Shedy.Core.Queries.GetCalendarEvent;

public class GetCalendarEventHandler : IRequestHandler<GetCalendarEvent, GetCalendarEventResult>
{
    private readonly ICalendarRepository _repository;

    public GetCalendarEventHandler(ICalendarRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetCalendarEventResult> Handle(GetCalendarEvent request, CancellationToken cancellationToken)
    {
        
        var calendar = await _repository.GetAsync(request.CalendarId, cancellationToken);
        Guard.Against.Null(calendar, nameof(calendar));
        var calendarEvent = calendar.Events.FirstOrDefault(x => x.Id == request.EventId);
        Guard.Against.Null(calendarEvent, nameof(calendarEvent));
        return new GetCalendarEventResult(calendarEvent);
    }
}