using Shedy.Core.Calendar;

namespace Shedy.Core.Handlers.UpdateAvailability;

public record UpdateAvailabilityResult(IEnumerable<Availability> OpeningHours);