using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Shedy.Core.Aggregates.Calendar;
using Shedy.Core.Builders;
using Shedy.Core.Commands.CreateOpeningTime;
using Shedy.Core.UnitTests.Mocks;

namespace Shedy.Core.UnitTests.Handlers;

public class CreateOpeningTimeHandlerShould
{
    [Theory]
    [AutoData]
    public async Task GetCalendarFromRepository(CreateOpeningTime command)
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

        var handler = new CreateOpeningTimeHandler(mockRepo.Object);

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
    public async Task AddAvailabilityToCalendar(CreateOpeningTime command)
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
        var handler = new CreateOpeningTimeHandler(mockRepo.Object);

        // act
        var result = await handler.Handle(command, default);

        // assert
        result.OpeningHours.First().Should().Be(command.OpeningTime);
    }

    [Theory]
    [AutoData]
    public async Task SaveNewCalendarToRepository(CreateOpeningTime command)
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
        var handler = new CreateOpeningTimeHandler(mockRepo.Object);

        // act
        var result = await handler.Handle(command, default);

        // assert
        mockRepo.Verify(x => x.UpdateAsync(
            It.Is<CalendarAggregate>(y => y.Id == command.CalendarId),
            It.IsAny<CancellationToken>()
        ));
    }
}