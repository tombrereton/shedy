using Ardalis.GuardClauses;
using Shedy.Core.Calendar;

namespace Shedy.Core.Builders;

public class CalendarEventBuilder
{
    private DateTimeOffset _start;
    private DateTimeOffset _finish;
    private TimeZoneInfo _timeZone = null!;

    public CalendarEventBuilder CreateCalendarEvent()
    {
        return new CalendarEventBuilder();
    }

    public CalendarEventBuilder WithStart(DateTimeOffset start)
    {
        _start = start;
        return this;
    }
    
    public CalendarEventBuilder WithFinish(DateTimeOffset finish)
    {
        _finish = finish;
        return this;
    }

    public CalendarEventBuilder WithDurationInMinutes(int minutes)
    {
        _finish = _start.AddMinutes(minutes);
        return this;
    }

    public CalendarEventBuilder WithTimeZone(TimeZoneInfo timeZone)
    {
        _timeZone = timeZone;
        return this;
    }

    public CalendarEvent Build()
    {
        Guard.Against.Null(_start, nameof(_start));
        Guard.Against.Null(_finish, nameof(_finish));
        Guard.Against.Null(_timeZone, nameof(_timeZone));
        
        var id = Guid.NewGuid();
        var title = "title";
        var description = "description";
        return new CalendarEvent(id, _start, _finish, _timeZone, title, description);
    }
}