using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Contracts.Responses;

public record CreateCalendarResponse(CalendarAggregate Calendar);