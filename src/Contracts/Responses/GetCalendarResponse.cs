using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Contracts.Responses;

public record GetCalendarResponse(CalendarAggregate Calendar);