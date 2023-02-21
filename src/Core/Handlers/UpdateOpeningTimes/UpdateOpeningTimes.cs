using MediatR;
using Shedy.Core.Calendar;

namespace Shedy.Core.Handlers.UpdateOpeningTimes;

public record UpdateOpeningTimes(
    Guid CalendarId,
    IEnumerable<OpeningTime> OpeningTimes
    ) : IRequest<UpdateOpeningTimesResult>;