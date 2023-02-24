namespace Shedy.Core.Aggregates.Calendar;

public class CalendarAggregate
{
    public Guid Id { get; }
    public Guid UserId { get; }

    private readonly List<OpeningTime> _openingTimes = new();
    private readonly List<CalendarEvent> _events = new();

    public IReadOnlyList<OpeningTime> OpeningTimes => _openingTimes.AsReadOnly();
    public IReadOnlyList<CalendarEvent> Events => _events.AsReadOnly();

    public CalendarAggregate(Guid id, Guid userId, List<OpeningTime> openingTimes)
    {
        Id = id;
        UserId = userId;
        _openingTimes = openingTimes;
    }

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
        var openingTimesOnDay = OpeningTimes.FirstOrDefault(x => x.Day == day);
        if (openingTimesOnDay is null || start < openingTimesOnDay.Start.ToTimeSpan())
            return false;
        return true;
    }

    // Keep empty constructor for EF Core
    private CalendarAggregate()
    {
    }
}