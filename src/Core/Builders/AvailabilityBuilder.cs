using Shedy.Core.Calendar;

namespace Shedy.Core.Builders;

public class AvailabilityBuilder
{
    private DayOfWeek _dayOfWeek;
    private TimeOnly _startTime;
    private TimeOnly _finishTime;
    private TimeZoneInfo _timeZone = null!;

    public AvailabilityBuilder CreateAvailability()
    {
        return new AvailabilityBuilder();
    }

    public AvailabilityBuilder WithDay(DayOfWeek dayOfWeek)
    {
        _dayOfWeek = dayOfWeek;
        return this;
    }

    public AvailabilityBuilder WithStartTime(TimeOnly time)
    {
        _startTime = time;
        return this;
    }

    public AvailabilityBuilder WithFinishTime(TimeOnly time)
    {
        _finishTime = time;
        return this;
    }

    public AvailabilityBuilder WithTimeZone(TimeZoneInfo timezone)
    {
        _timeZone = timezone;
        return this;
    }
    
    public AvailabilityBuilder WithDefaultStartAndFinishTimes()
    {
        _startTime = new TimeOnly(9, 0);
        _finishTime = new TimeOnly(17, 0);
        
        return this;
    }

    public Availability Build()
    {
        return new Availability(_dayOfWeek, _startTime, _finishTime, _timeZone);
    }

}