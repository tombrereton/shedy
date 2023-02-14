namespace Shedy.Core.Calendar;

public record Availability(
    DayOfWeek Day,
    TimeOnly Start,
    TimeOnly Finish,
    TimeZoneInfo TimeZone
);