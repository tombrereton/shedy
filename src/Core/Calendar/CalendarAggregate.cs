namespace Shedy.Core.Calendar;

public class CalendarAggregate
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }

    public IReadOnlyList<Availability> OpeningHours { get; init; }
    // private readonly List<Availability> _openingHours;

    // public CalendarAggregate(Guid id, Guid userId, List<Availability> openingHours)
    // {
    //     Id = id;
    //     UserId = userId;
    //     // _openingHours = openingHours;
    //     OpeningHours = openingHours.AsReadOnly();
    // }

    public void AddAvailability(Availability availability)
    {
        var openingHours = OpeningHours.ToList();
        openingHours.Add(availability);
        // OpeningHours = openingHours.AsReadOnly();
    }
}