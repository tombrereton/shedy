using Ardalis.GuardClauses;
using Shedy.Domain.Aggregates.Calendar;

namespace Shedy.Domain.Builders;

public class OpeningTimesBuilder
{
    private TimeOnly _startTime;
    private TimeOnly _finishTime;
    private List<DayOfWeek> _days = new();
    private TimeZoneInfo _timeZone = null!;

    public OpeningTimesBuilder CreateOpeningTimes()
    {
        return new OpeningTimesBuilder();
    }

    public OpeningTimesBuilder WithDefaultDays()
    {
        _days = new List<DayOfWeek>()
            { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
        return this;
    }

    public OpeningTimesBuilder WithDay(DayOfWeek day)
    {
        _days.Add(day);
        return this;
    }

    public OpeningTimesBuilder WithDefaultStartAndFinishTimes()
    {
        _startTime = new TimeOnly(9, 0);
        _finishTime = new TimeOnly(17, 0);

        return this;
    }

    public OpeningTimesBuilder WithTimeZone(TimeZoneInfo timeZone)
    {
        _timeZone = timeZone;
        return this;
    }


    public List<OpeningTime> Build()
    {
        Guard.Against.Null(_days, nameof(_days));
        Guard.Against.Null(_startTime, nameof(_startTime));
        Guard.Against.Null(_finishTime, nameof(_finishTime));
        Guard.Against.Null(_timeZone, nameof(_timeZone));
        
        var openingHours = new List<OpeningTime>();
        foreach (var day in _days)
        {
            var availability = new OpeningTimeBuilder()
                .CreateOpeningTime()
                .WithDay(day)
                .WithStartTime(_startTime)
                .WithFinishTime(_finishTime)
                .WithTimeZone(_timeZone)
                .Build();
            openingHours.Add(availability);
        }

        return openingHours;
    }
}