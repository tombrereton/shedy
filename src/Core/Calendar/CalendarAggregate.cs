namespace Shedy.Core.Calendar;

public class CalendarAggregate
{
    public IEnumerable<TimeSlot> GetAvailableTimes(DateTimeOffset from, DateTimeOffset to, int skip, int take)
    {
        return new List<TimeSlot>();
    }

    public IEnumerable<Availability> GetAvailability()
    {
        return new List<Availability>();
    }
}

public record Availability(DayOfWeek Day, TimeSpan Start, TimeSpan Finish);

public record TimeSlot(DateTimeOffset From, DateTimeOffset To);