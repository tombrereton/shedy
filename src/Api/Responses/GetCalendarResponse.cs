using Shedy.Core.Aggregates.Calendar;

namespace Shedy.Api.Responses;

public record GetCalendarResponse(CalendarAggregate Calendar);