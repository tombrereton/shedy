using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Application.Commands.CreateCalendar;

public record CreateCalendarResult(Guid UserId, Guid CalendarId, CalendarAggregate Calendar);