using Ardalis.GuardClauses;
using Shedy.Core.Aggregates.Calendar;
using Shedy.Core.Builders;

namespace Shedy.Core.Domain.Builders;

public class CalendarBuilder
{
    private Guid _calendarId;
    private List<OpeningTime> _openingHours = new();
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

    public CalendarBuilder WithDefaultOpeningHours(TimeZoneInfo timeZone)
    {
        _openingHours = new OpeningTimesBuilder()
            .CreateOpeningTimes()
            .WithDefaultDays()
            .WithDefaultStartAndFinishTimes()
            .WithTimeZone(timeZone)
            .Build();
            
        return this;
    }

    public CalendarAggregate Build()
    {
        Guard.Against.Null(_openingHours, nameof(_openingHours));
        Guard.Against.NullOrEmpty(_calendarId, nameof(_calendarId));
        Guard.Against.NullOrEmpty(_userId, nameof(_userId));

        return new CalendarAggregate(_calendarId, _userId, _openingHours);
    }
}