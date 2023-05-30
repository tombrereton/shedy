using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Application.Handlers.Commands.CreateOpeningTime;

public record CreateOpeningTimeResult(IEnumerable<OpeningTime> OpeningHours);