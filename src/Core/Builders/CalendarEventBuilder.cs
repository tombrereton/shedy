using Shedy.Core.Calendar;

namespace Shedy.Core.Builders;

public class CalendarEventBuilder
{
    public CalendarEventBuilder CreateCalendarEvent()
    {
        return new CalendarEventBuilder();
    }

    public CalendarEvent Build()
    {
        var id = Guid.NewGuid();
        var start = DateTimeOffset.Now;
        var finish = DateTimeOffset.Now.AddMinutes(30);
        var timeZone = TimeZoneInfo.Local;
        var title = "title";
        var description = "description";
        return new CalendarEvent(id, start, finish, timeZone, title, description);
    }
}