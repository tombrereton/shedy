using MediatR;

namespace Shedy.Core.Queries.GetCalendarEvent;

public record GetCalendarEvent(
    Guid CalendarId,
    Guid EventId
    ) : IRequest<GetCalendarEventResult>;