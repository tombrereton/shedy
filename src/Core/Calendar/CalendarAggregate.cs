using Shedy.Core.Handlers.CreateCalendar;

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

    // Keep empty constructor for EF Core
    private CalendarAggregate()
    {
    }
}