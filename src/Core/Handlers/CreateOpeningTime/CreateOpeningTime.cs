using MediatR;
using Shedy.Core.Calendar;

namespace Shedy.Core.Handlers.CreateOpeningTime;

public record CreateOpeningTime(
    Guid CalendarId,
    OpeningTime OpeningTime
    ) : IRequest<CreateOpeningTimeResult>;