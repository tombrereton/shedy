namespace Shedy.Core.Calendar;

public class CalendarAggregate
{
    public Guid Id { get; private set; }
    private readonly List<Availability> _openingHours;

    public CalendarAggregate(Guid id, List<Availability> openingHours)
    {
        Id = id;
        _openingHours = openingHours;
    }

    public IEnumerable<TimeSlot> GetAvailableTimes(DateTimeOffset from, DateTimeOffset to, int skip, int take)
    {
        return new List<TimeSlot>();
    }

    public IReadOnlyList<Availability> GetOpeningHours()
    {
        return _openingHours;
    }

    public void AddAvailability(Availability availability)
    {
        _openingHours.Add(availability);
    }
}

public record Availability(DayOfWeek Day, TimeOnly Start, TimeOnly Finish, TimeZoneInfo TimeZone);

public record TimeSlot(DateTimeOffset From, DateTimeOffset To);