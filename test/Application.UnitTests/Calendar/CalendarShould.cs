using FluentAssertions;
using Shedy.Domain.Builders;

namespace Shedy.Core.UnitTests.Calendar;

public class CalendarShould
{
    [Theory]
    [InlineData("2023-01-01T06:00:00Z", "day is on a weekend")]
    [InlineData("2023-01-02T08:59:00Z", "correct day but before 9am")]
    public void NotAddEventOutsideOpeningHours(string startDateTime, string because)
    {
        // arrange 
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithDefaultOpeningHours(TimeZoneInfo.Local)
            .Build();
        var start = DateTimeOffset.Parse(startDateTime);
        var calendarEvent = new CalendarEventBuilder()
            .CreateCalendarEvent()
            .WithStart(start)
            .WithFinish(start.AddMinutes(30))
            .WithTimeZone(TimeZoneInfo.Local)
            .Build();

        // act
        var act = () => calendar.AddEvent(calendarEvent);

        // assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("*Event not within opening hours*", because);
    }
}