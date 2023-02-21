using Shedy.Core.Calendar;

namespace Shedy.Core.Handlers.CreateOpeningTime;

public record CreateOpeningTimeResult(IEnumerable<OpeningTime> OpeningHours);