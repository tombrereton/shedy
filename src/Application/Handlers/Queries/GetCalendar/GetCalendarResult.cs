using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Application.Handlers.Queries.GetCalendar;

public record GetCalendarResult(CalendarAggregate Calendar);