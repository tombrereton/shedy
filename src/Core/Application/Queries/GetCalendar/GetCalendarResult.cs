using Shedy.Core.Aggregates.Calendar;
using Shedy.Core.Domain.Aggregates.Calendar;

namespace Shedy.Core.Queries.GetCalendar;

public record GetCalendarResult(CalendarAggregate Calendar);