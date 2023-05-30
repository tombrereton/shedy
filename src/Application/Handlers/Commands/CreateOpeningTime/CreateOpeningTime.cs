using MediatR;
using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Application.Handlers.Commands.CreateOpeningTime;

public record CreateOpeningTime(
    Guid CalendarId,
    OpeningTime OpeningTime
    ) : IRequest<CreateOpeningTimeResult>;