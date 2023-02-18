using Ardalis.GuardClauses;
using Moq;
using Shedy.Core.Calendar;
using Shedy.Core.Interfaces;

namespace Shedy.Core.UnitTests.Mocks;

public class MockCalendarRepository : Mock<ICalendarRepository>
{
    private Guid _id;
    private CalendarAggregate _output = null!;

    public MockCalendarRepository CreateGetAsyncStub()
    {
        return this;
    }

    public MockCalendarRepository WithInputParameters(Guid calendarId)
    {
        _id = calendarId;
        return this;
    }

    public MockCalendarRepository WithReturnObject(CalendarAggregate calendar)
    {
        _output = calendar;
        return this;
    }

    public MockCalendarRepository Build()
    {
        Guard.Against.NullOrEmpty(_id, nameof(_id));
        Guard.Against.Null(_output, nameof(_output));
        
        Setup(x => x.GetAsync(
            It.Is<Guid>(y => y == _id),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(_output);

        return this;
    }
}