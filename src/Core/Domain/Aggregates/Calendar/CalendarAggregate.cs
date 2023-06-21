using Shedy.Core.Aggregates.Calendar;

namespace Shedy.Core.Domain.Aggregates.Calendar;

public class CalendarAggregate
{
    private readonly List<OpeningTime> _openingTimes = new();
    private readonly List<CalendarEvent> _events = new();

    public CalendarAggregate(Guid id, Guid userId, List<OpeningTime> openingTimes, List<CalendarEvent> events)
    {
        Id = id;
        UserId = userId;
        _openingTimes = openingTimes;
        _events = events;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
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
        // if (!IsValidEvent(calendarEvent))
        //     throw new ArgumentException("Event not within opening hours");
        _events.Clear();
        _events.Add(calendarEvent);
    }

    private bool IsValidEvent(CalendarEvent calendarEvent)
    {
        // var day = calendarEvent.Start.DayOfWeek;
        // var start = calendarEvent.Start.TimeOfDay;
        List<string> s = new();
        // IEnumerable<string> c = new();
        var d = new List<string>();
        // var openingTimesOnDay = OpeningTimes.FirstOrDefault(x => x.Day == day);
        // if (openingTimesOnDay is null || start < openingTimesOnDay.Start.ToTimeSpan())
            return false;
        return true;
    }

    // Keep empty constructor for EF Core
    private CalendarAggregate()
    {
    }
}