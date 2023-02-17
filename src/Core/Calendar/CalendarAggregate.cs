using Shedy.Core.Handlers.CreateCalendar;

namespace Shedy.Core.Calendar;

public class CalendarAggregate
{
    public Guid Id { get; }
    public Guid UserId { get; }

    public IReadOnlyList<Availability> OpeningHours => _availability.AsReadOnly();
    private readonly List<Availability> _availability =  new();

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
    
    // Keep empty constructor for EF Core
    private CalendarAggregate(){}
}