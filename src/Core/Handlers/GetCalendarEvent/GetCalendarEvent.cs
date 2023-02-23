using MediatR;
using Shedy.Core.Calendar;

namespace Shedy.Core.Handlers.GetCalendarEvent;

public record GetCalendarEvent(
    Guid CalendarId,
    Guid EventId
    ) : IRequest<GetCalendarEventResult>;