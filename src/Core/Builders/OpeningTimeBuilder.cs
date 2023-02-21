using Shedy.Core.Calendar;

namespace Shedy.Core.Builders;

public class OpeningTimeBuilder
{
    private DayOfWeek _dayOfWeek;
    private TimeOnly _startTime;
    private TimeOnly _finishTime;
    private TimeZoneInfo _timeZone = null!;

    public OpeningTimeBuilder CreateOpeningTime()
    {
        return new OpeningTimeBuilder();
    }

    public OpeningTimeBuilder WithDay(DayOfWeek dayOfWeek)
    {
        _dayOfWeek = dayOfWeek;
        return this;
    }

    public OpeningTimeBuilder WithStartTime(TimeOnly time)
    {
        _startTime = time;
        return this;
    }

    public OpeningTimeBuilder WithFinishTime(TimeOnly time)
    {
        _finishTime = time;
        return this;
    }

    public OpeningTimeBuilder WithTimeZone(TimeZoneInfo timezone)
    {
        _timeZone = timezone;
        return this;
    }
    
    public OpeningTimeBuilder WithDefaultStartAndFinishTimes()
    {
        _startTime = new TimeOnly(9, 0);
        _finishTime = new TimeOnly(17, 0);
        
        return this;
    }

    public OpeningTime Build()
    {
        return new OpeningTime(_dayOfWeek, _startTime, _finishTime, _timeZone);
    }

}