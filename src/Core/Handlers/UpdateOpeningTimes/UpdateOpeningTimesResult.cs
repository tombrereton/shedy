using Shedy.Core.Aggregates.Calendar;

namespace Shedy.Core.Handlers.UpdateOpeningTimes;

public record UpdateOpeningTimesResult(IEnumerable<OpeningTime> OpeningHours);