using Shedy.Core.Aggregates.Calendar;

namespace Shedy.Core.Commands.CreateCalendar;

public record CreateCalendarResult(Guid UserId, Guid CalendarId, CalendarAggregate Calendar);