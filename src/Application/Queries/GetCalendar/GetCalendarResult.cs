using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Application.Queries.GetCalendar;

public record GetCalendarResult(CalendarAggregate Calendar);