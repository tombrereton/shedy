using Shedy.Core.Aggregates.Calendar;
using Shedy.Core.Domain.Aggregates.Calendar;

namespace Shedy.Api.Responses;

public record GetCalendarResponse(CalendarAggregate Calendar);