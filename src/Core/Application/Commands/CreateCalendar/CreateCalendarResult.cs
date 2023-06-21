using Shedy.Core.Aggregates.Calendar;
using Shedy.Core.Domain.Aggregates.Calendar;

namespace Shedy.Core.Commands.CreateCalendar;

public record CreateCalendarResult(Guid UserId, Guid CalendarId, CalendarAggregate Calendar);