using Shedy.Core.Aggregates.Calendar;

namespace Shedy.Core.Commands.CreateOpeningTime;

public record CreateOpeningTimeResult(IEnumerable<OpeningTime> OpeningHours);