using MediatR;
using Shedy.Core.Calendar;

namespace Shedy.Core.Handlers.AddCalendarEvent;

public record AddCalendarEvent(
    Guid CalendarId,
    CalendarEvent Event
    ) : IRequest<AddCalendarEventResult>;