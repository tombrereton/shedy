namespace Shedy.Core.Calendar;

public class CalendarAggregate
{
    private readonly List<Availability> _availability;

    public CalendarAggregate(List<Availability> availability)
    {
        _availability = availability;
    }

    public IEnumerable<TimeSlot> GetAvailableTimes(DateTimeOffset from, DateTimeOffset to, int skip, int take)
    {
        return new List<TimeSlot>();
    }

    public IReadOnlyList<Availability> GetAvailability()
    {
        return _availability;
    }

    public void AddAvailability(Availability availability)
    {
        _availability.Add(availability);
    }
}

public record Availability(DayOfWeek Day, TimeOnly Start, TimeOnly Finish, TimeZoneInfo TimeZone);

public record TimeSlot(DateTimeOffset From, DateTimeOffset To);