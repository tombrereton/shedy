using FluentAssertions;
using Shedy.Core.Calendar;

namespace Shedy.Core.UnitTests.Calendar;

public class CalendarShould
{
    [Fact]
    public void GetAvailableTimesForUser()
    {
        // arrange 
        var calendar = new CalendarAggregate(new List<Availability>());
        var from = DateTimeOffset.Now;
        var to = DateTimeOffset.Now.AddDays(7);
        var skip = 0;
        var take = 10;
        
        // act 
        var actual = calendar.GetAvailableTimes(from, to, skip, take);
        
        // assert 
        actual.Should().BeAssignableTo<IEnumerable<TimeSlot>>();
        // actual.First().From.Should().Be(DateTimeOffset.)
    }

}