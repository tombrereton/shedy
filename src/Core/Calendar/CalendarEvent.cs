namespace Shedy.Core.Calendar;

public record CalendarEvent(
    Guid Id,
    DateTimeOffset Start,
    DateTimeOffset Finish,
    TimeZoneInfo TimeZone,
    string Title,
    string Description
);