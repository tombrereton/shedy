namespace Shedy.Core.Calendar;

public class CalendarAggregate
{
    public Guid Id { get; }
    public Guid UserId { get; }

    private readonly List<Availability> _availability = new();
    private readonly List<CalendarEvent> _events = new();

    public IReadOnlyList<Availability> OpeningHours => _availability.AsReadOnly();
    public IReadOnlyList<CalendarEvent> Events => _events.AsReadOnly();

    public CalendarAggregate(Guid id, Guid userId, List<Availability> availability)
    {
        Id = id;
        UserId = userId;
        _availability = availability;
    }

    public void AddAvailability(Availability availability)
    {
        _availability.Add(availability);
    }

    public void UpdateAvailability(IEnumerable<Availability> newAvailability)
    {
        _availability.Clear();
        _availability.AddRange(newAvailability);
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
        var openingHoursOnDay = OpeningHours.FirstOrDefault(x => x.Day == day);
        if (openingHoursOnDay is null || start < openingHoursOnDay.Start.ToTimeSpan())
            return false;
        return true;
    }

    // Keep empty constructor for EF Core
    private CalendarAggregate()
    {
    }
}