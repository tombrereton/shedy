using MediatR;
using Shedy.Core.Aggregates.Calendar;

namespace Shedy.Core.Commands.UpdateOpeningTimes;

public record UpdateOpeningTimes(
    Guid CalendarId,
    IEnumerable<OpeningTime> OpeningTimes
    ) : IRequest<UpdateOpeningTimesResult>;