using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Application.Commands.UpdateOpeningTimes;

public record UpdateOpeningTimesResult(IEnumerable<OpeningTime> OpeningHours);