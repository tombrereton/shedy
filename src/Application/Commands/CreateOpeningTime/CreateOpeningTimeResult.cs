using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Application.Commands.CreateOpeningTime;

public record CreateOpeningTimeResult(IEnumerable<OpeningTime> OpeningHours);