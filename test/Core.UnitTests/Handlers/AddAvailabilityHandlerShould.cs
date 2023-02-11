using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Shedy.Core.Calendar;
using Shedy.Core.Handlers.AddAvailability;
using Shedy.Core.UnitTests.Mocks;

namespace Shedy.Core.UnitTests.Handlers;

public class AddAvailabilityHandlerShould
{
    [Theory]
    [AutoData]
    public async Task AddAvailability(AddAvailability command)
    {
        // arrange
        var calendar = MakeCalendarWithEmptyAvailability();
        var mockRepo = new MockCalendarRepository().MockGetAsync(calendar, command.CalendarId);
        var handler = new AddAvailabilityHandler(mockRepo.Object);

        // act
        var result = await handler.Handle(command, default);

        // assert
        result.Availability.First().Should().Be(command.Availability);
    }


    [Theory]
    [AutoData]
    public async Task GetCalendarFromRepository(AddAvailability command)
    {
        // act
        var calendar = MakeCalendarWithEmptyAvailability();
        var mockRepo = new MockCalendarRepository().MockGetAsync(calendar, command.CalendarId);
        var handler = new AddAvailabilityHandler(mockRepo.Object);

        // act
        var result = await handler.Handle(command, default);

        // assert
        mockRepo.Verify(x => x.GetAsync(
            It.Is<Guid>(y => y == command.CalendarId),
            It.IsAny<CancellationToken>()
        ));
    }

    private static CalendarAggregate MakeCalendarWithEmptyAvailability()
    {
        var calendar = new CalendarAggregate(new List<Availability>());
        return calendar;
    }
}