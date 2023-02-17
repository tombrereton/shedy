using Shedy.Core.Calendar;

namespace Shedy.Core.Builders;

public class CalendarBuilder
{
    private Guid _calendarId;
    private List<Availability> _openingHours = new();
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
        return this;
    }

    public CalendarBuilder WithUserId(Guid userId)
    {
        _userId = userId;
        return this;
    }

    public CalendarBuilder WithDefaultOpeningHours()
    {
        var days = new List<DayOfWeek>()
            { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };

        var openingHours = new List<Availability>();
        foreach (var day in days)
        {
            var availability = new Availability(
                day,
                new TimeOnly(9, 0),
                new TimeOnly(17, 0),
                TimeZoneInfo.Local // change to users local
            );
            openingHours.Add(availability);
        }

        _openingHours = openingHours;

        return this;
    }

    public CalendarAggregate Build()
    {
        if (_openingHours is null) throw new ArgumentException("Opening Hours cannot be null");

        return new CalendarAggregate(_calendarId, _userId, _openingHours);
        // return new CalendarAggregate
        // {
        //     Id = _calendarId,
        //     UserId = _userId,
        //     OpeningHours = _openingHours
        // };
    }
}