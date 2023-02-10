using MediatR;
using Shedy.Core.Calendar;

namespace Shedy.Core.Handlers.AddAvailability;

public record AddAvailability(
    Guid CalendarId,
    Availability Availability
    ) : IRequest<AddAvailabilityResult>;