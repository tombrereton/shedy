using Shedy.Core.Calendar;

namespace Shedy.Core.Handlers.AddCalendarEvent;

public record AddCalendarEventResult(Guid CalendarId, CalendarEvent Event);