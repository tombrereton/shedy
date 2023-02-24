using MediatR;
using Shedy.Core.Aggregates.Calendar;

namespace Shedy.Core.Handlers.UpdateOpeningTimes;

public record UpdateOpeningTimes(
    Guid CalendarId,
    IEnumerable<OpeningTime> OpeningTimes
    ) : IRequest<UpdateOpeningTimesResult>;