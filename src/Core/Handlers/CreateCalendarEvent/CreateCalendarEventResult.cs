using Shedy.Core.Calendar;

namespace Shedy.Core.Handlers.CreateCalendarEvent;

public record CreateCalendarEventResult(Guid CalendarId, CalendarEvent Event);