using Moq;
using Shedy.Core.Calendar;
using Shedy.Core.Interfaces;

namespace Shedy.Core.UnitTests.Mocks;

public class MockCalendarRepositoryBuilder : Mock<ICalendarRepository>
{
    public MockCalendarRepositoryBuilder WithStubbedGetAsync(CalendarAggregate output, Guid id)
    {
        Setup(x => x.GetAsync(
            It.Is<Guid>(y => y == id),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(output);

        return this;
    }
}