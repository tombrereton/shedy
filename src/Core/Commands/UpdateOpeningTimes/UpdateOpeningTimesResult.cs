using Shedy.Core.Aggregates.Calendar;

namespace Shedy.Core.Commands.UpdateOpeningTimes;

public record UpdateOpeningTimesResult(IEnumerable<OpeningTime> OpeningHours);