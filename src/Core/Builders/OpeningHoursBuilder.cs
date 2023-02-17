using Ardalis.GuardClauses;
using Shedy.Core.Calendar;

namespace Shedy.Core.Builders;

public class OpeningHoursBuilder
{
    private TimeOnly _startTime;
    private TimeOnly _finishTime;
    private List<DayOfWeek> _days = new();
    private TimeZoneInfo _timeZone;

    public OpeningHoursBuilder CreateOpeningHours()
    {
        return new OpeningHoursBuilder();
    }

    public OpeningHoursBuilder WithDefaultDays()
    {
        _days = new List<DayOfWeek>()
            { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
        return this;
    }

    public OpeningHoursBuilder WithDay(DayOfWeek day)
    {
        _days.Add(day);
        return this;
    }

    public OpeningHoursBuilder WithDefaultStartAndFinishTimes()
    {
        _startTime = new TimeOnly(9, 0);
        _finishTime = new TimeOnly(17, 0);

        return this;
    }

    public OpeningHoursBuilder WithTimeZone(TimeZoneInfo timeZone)
    {
        _timeZone = timeZone;
        return this;
    }


    public List<Availability> Build()
    {
        Guard.Against.Null(_days, nameof(_days));
        Guard.Against.Null(_startTime, nameof(_startTime));
        Guard.Against.Null(_finishTime, nameof(_finishTime));
        Guard.Against.Null(_timeZone, nameof(_timeZone));
        
        var openingHours = new List<Availability>();
        foreach (var day in _days)
        {
            var availability = new AvailabilityBuilder()
                .CreateAvailability()
                .WithDay(day)
                .WithDefaultStartAndFinishTimes()
                .WithTimeZone(_timeZone)
                .Build();
            openingHours.Add(availability);
        }

        return openingHours;
    }
}