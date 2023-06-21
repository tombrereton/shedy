namespace Shedy.Core.Aggregates.Calendar;

public record CalendarEvent(
    Guid Id,
    // DateTimeOffset Start,
    // DateTimeOffset Finish,
    // TimeZoneInfo TimeZone,
    string Title
    // string Notes,
    // string Url,
    // string Location
    // Recurrence Recurrence,
    // IEnumerable<Attendee> Attendees
    // Alert? Alert = null
);

public record Attendee(
    string Name,
    string Email,
    Rsvp Rsvp = Rsvp.Pending
);

public enum Rsvp
{
    Pending,
    Accepted,
    Rejected
}

public record Recurrence(
    bool Enabled,
    RecurringUnit? Unit = null,
    int? Frequency = null
);

public enum RecurringUnit
{
    Day,
    Week,
    Monthly,
    Yearly
}

public record Alert(
    IEnumerable<TimeBefore> TimeBefore,
    IEnumerable<AlertType> Types
);

public enum AlertType
{
    Mobile,
    Email
}

public enum TimeBefore
{
    ZeroMin,
    FifteenMin,
    ThirtyMin,
    OneHour
}