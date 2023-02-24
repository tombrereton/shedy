using Shedy.Core.Aggregates.Calendar;

namespace Shedy.Core.Handlers.CreateCalendarEvent;

public record CreateCalendarEventResult(Guid CalendarId, CalendarEvent Event);