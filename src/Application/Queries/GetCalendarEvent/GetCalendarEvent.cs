using MediatR;

namespace Shedy.Application.Queries.GetCalendarEvent;

public record GetCalendarEvent(
    Guid CalendarId,
    Guid EventId
    ) : IRequest<GetCalendarEventResult>;