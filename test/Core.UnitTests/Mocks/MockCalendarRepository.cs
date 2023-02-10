using Moq;
using Shedy.Core.Calendar;
using Shedy.Core.Interfaces;

namespace Shedy.Core.UnitTests.Mocks;

public class MockCalendarRepository : Mock<ICalendarRepository>
{
    public MockCalendarRepository MockSaveAsync(CalendarAggregate output, Guid id)
    {
        Setup(x => x.GetAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        )).Returns<CalendarAggregate>(_ => Task.FromResult(output));
        return this;
    }
}