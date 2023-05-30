namespace Shedy.Domain.Aggregates.Calendar;

public interface IRepository<T> where T : IAggregateRoot
{
    void Add(T item);
    T Find(Predicate<T> match);
    void Remove(T item);

    void SaveChanges();
}

public interface IAggregateRoot
{
}

public class CalendarAggregate
{
    private readonly List<OpeningTime> _openingTimes = new();
    private readonly List<CalendarEvent> _events = new();

    public CalendarAggregate(Guid id, Guid userId, List<OpeningTime> openingTimes, uint version = 1)
    {
        Id = id;
        UserId = userId;
        _openingTimes = openingTimes;
        Version = version;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public uint Version { get; }
    public IReadOnlyList<OpeningTime> OpeningTimes => _openingTimes.AsReadOnly();
    public IReadOnlyList<CalendarEvent> Events => _events.AsReadOnly();

    public void AddOpeningTime(OpeningTime openingTime)
    {
        _openingTimes.Add(openingTime);
    }

    public void UpdateOpeningTimes(IEnumerable<OpeningTime> newAvailability)
    {
        _openingTimes.Clear();
        _openingTimes.AddRange(newAvailability);
    }

    public void AddEvent(CalendarEvent calendarEvent)
    {
        if (!IsValidEvent(calendarEvent))
            throw new ArgumentException("Event not within opening hours");
        _events.Add(calendarEvent);
    }

    private bool IsValidEvent(CalendarEvent calendarEvent)
    {
        var day = calendarEvent.Start.DayOfWeek;
        var start = calendarEvent.Start.TimeOfDay;
        List<string> s = new();
        // IEnumerable<string> c = new();
        var d = new List<string>();
        var openingTimesOnDay = OpeningTimes.FirstOrDefault(x => x.Day == day);
        if (openingTimesOnDay is null || start < openingTimesOnDay.Start.ToTimeSpan())
            return false;
        return true;
    }

    // Keep empty constructor for EF Domain
    private CalendarAggregate(uint version)
    {
        Version = version;
    }
}