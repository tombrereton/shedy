namespace Shedy.Core.Calendar;

public record OpeningTime(
    DayOfWeek Day,
    TimeOnly Start,
    TimeOnly Finish,
    TimeZoneInfo TimeZone
);