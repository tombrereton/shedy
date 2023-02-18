using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Shedy.Core.Builders;
using Shedy.Core.Calendar;
using Shedy.Core.Handlers.AddAvailability;
using Shedy.Core.UnitTests.Mocks;

namespace Shedy.Core.UnitTests.Handlers;

public class AddAvailabilityHandlerShould
{
    [Theory]
    [AutoData]
    public async Task GetCalendarFromRepository(AddAvailability command)
    {
        // act
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithCalendarId(command.CalendarId)
            .WithUserId(Guid.NewGuid())
            .WithEmptyOpeningHours()
            .Build();
        var mockRepo = new MockCalendarRepository()
            .CreateGetAsyncStub()
            .WithInputParameters(calendar.Id)
            .WithReturnObject(calendar)
            .Build();
        
        var handler = new AddAvailabilityHandler(mockRepo.Object);

        // act
        await handler.Handle(command, default);

        // assert
        mockRepo.Verify(x => x.GetAsync(
            It.Is<Guid>(y => y == command.CalendarId),
            It.IsAny<CancellationToken>()
        ));
    }

    [Theory]
    [AutoData]
    public async Task AddAvailabilityToCalendar(AddAvailability command)
    {
        // arrange
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithUserId(Guid.NewGuid())
            .WithCalendarId(command.CalendarId)
            .WithEmptyOpeningHours()
            .Build();
        var mockRepo = new MockCalendarRepository()
            .CreateGetAsyncStub()
            .WithInputParameters(calendar.Id)
            .WithReturnObject(calendar)
            .Build();
        var handler = new AddAvailabilityHandler(mockRepo.Object);

        // act
        var result = await handler.Handle(command, default);

        // assert
        result.OpeningHours.First().Should().Be(command.Availability);
    }

    [Theory]
    [AutoData]
    public async Task SaveNewCalendarToRepository(AddAvailability command)
    {
        // act
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithCalendarId(command.CalendarId)
            .WithUserId(Guid.NewGuid())
            .WithEmptyOpeningHours()
            .Build();
        var mockRepo = new MockCalendarRepository()
            .CreateGetAsyncStub()
            .WithInputParameters(calendar.Id)
            .WithReturnObject(calendar)
            .Build();
        var handler = new AddAvailabilityHandler(mockRepo.Object);

        // act
        var result = await handler.Handle(command, default);

        // assert
        mockRepo.Verify(x => x.SaveAsync(
            It.Is<CalendarAggregate>(y => y.Id == command.CalendarId && y.OpeningHours.Count == 1),
            It.IsAny<CancellationToken>()
        ));
    }
}