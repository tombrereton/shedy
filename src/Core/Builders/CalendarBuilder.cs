using Shedy.Core.Calendar;

namespace Shedy.Core.Builders;

public class CalendarBuilder
{
    private Guid _calendarId;
    private List<Availability> _openingHours = null!;
    private Guid _userId;

    public CalendarBuilder CreateCalendar()
    {
        return new CalendarBuilder();
    }

    public CalendarBuilder WithCalendarId(Guid id)
    {
        _calendarId = id;
        return this;
    }
    
    public CalendarBuilder WithNewCalendarId()
    {
        _calendarId = Guid.NewGuid();
        return this;
    }

    public CalendarBuilder WithEmptyOpeningHours()
    {
        _openingHours = new List<Availability>();
        return this;
    }
    
    public CalendarBuilder WithUserId(Guid userId)
    {
        _userId = userId;
        return this;
    }

    public CalendarAggregate Build()
    {
        if (_openingHours is null) throw new ArgumentException("Opening Hours cannot be null");
        
        return new CalendarAggregate(_calendarId, _userId, _openingHours);
    }

}