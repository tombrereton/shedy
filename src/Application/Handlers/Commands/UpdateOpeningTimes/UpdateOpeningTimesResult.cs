using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Application.Handlers.Commands.UpdateOpeningTimes;

public record UpdateOpeningTimesResult(IEnumerable<OpeningTime> OpeningHours);