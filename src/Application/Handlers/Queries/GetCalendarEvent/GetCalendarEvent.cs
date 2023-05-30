using MediatR;

namespace Shedy.Application.Handlers.Queries.GetCalendarEvent;

public record GetCalendarEvent(
    Guid CalendarId,
    Guid EventId
    ) : IRequest<GetCalendarEventResult>;