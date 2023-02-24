using MediatR;
using Shedy.Core.Aggregates.Calendar;

namespace Shedy.Core.Handlers.CreateOpeningTime;

public record CreateOpeningTime(
    Guid CalendarId,
    OpeningTime OpeningTime
    ) : IRequest<CreateOpeningTimeResult>;