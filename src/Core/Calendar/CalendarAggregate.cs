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

    public void AddAvailability(Availability availability)
    {
        throw new NotImplementedException();
    }
}

public record Availability(DayOfWeek Day, TimeOnly Start, TimeOnly Finish, TimeZoneInfo TimeZone);

public record TimeSlot(DateTimeOffset From, DateTimeOffset To);