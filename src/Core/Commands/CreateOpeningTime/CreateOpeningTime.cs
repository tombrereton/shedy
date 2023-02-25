using MediatR;
using Shedy.Core.Aggregates.Calendar;

namespace Shedy.Core.Commands.CreateOpeningTime;

public record CreateOpeningTime(
    Guid CalendarId,
    OpeningTime OpeningTime
    ) : IRequest<CreateOpeningTimeResult>;