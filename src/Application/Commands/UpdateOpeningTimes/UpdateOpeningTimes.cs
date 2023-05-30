using MediatR;
using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Application.Commands.UpdateOpeningTimes;

public record UpdateOpeningTimes(
    Guid CalendarId,
    IEnumerable<OpeningTime> OpeningTimes
    ) : IRequest<UpdateOpeningTimesResult>;