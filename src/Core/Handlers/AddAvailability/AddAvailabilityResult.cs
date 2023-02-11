using Shedy.Core.Calendar;

namespace Shedy.Core.Handlers.AddAvailability;

public record AddAvailabilityResult(IEnumerable<Availability> OpeningHours);