using MediatR;
using Shedy.Core.Calendar;

namespace Shedy.Core.Handlers.UpdateAvailability;

public record UpdateAvailability(
    Guid CalendarId,
    IEnumerable<Availability> Availability
    ) : IRequest<UpdateAvailabilityResult>;