using Shedy.Core.Aggregates.Calendar;

namespace Shedy.Core.Commands.CreateCalendarEvent;

public record CreateCalendarEventResult(Guid CalendarId, CalendarEvent Event);