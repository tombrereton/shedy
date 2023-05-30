using MediatR;
using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Application.Commands.CreateOpeningTime;

public record CreateOpeningTime(
    Guid CalendarId,
    OpeningTime OpeningTime
    ) : IRequest<CreateOpeningTimeResult>;