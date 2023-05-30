using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Api.Responses;

public record CreateCalendarResponse(CalendarAggregate Calendar);