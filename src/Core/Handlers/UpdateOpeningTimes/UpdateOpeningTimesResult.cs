using Shedy.Core.Calendar;

namespace Shedy.Core.Handlers.UpdateOpeningTimes;

public record UpdateOpeningTimesResult(IEnumerable<OpeningTime> OpeningHours);