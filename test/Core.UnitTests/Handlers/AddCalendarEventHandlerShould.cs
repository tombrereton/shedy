using AutoFixture.Xunit2;
using Moq;
using Shedy.Core.Builders;
using Shedy.Core.Calendar;
using Shedy.Core.Handlers.AddCalendarEvent;
using Shedy.Core.UnitTests.Mocks;

namespace Shedy.Core.UnitTests.Handlers;

public class AddCalendarEventHandlerShould
{
    [Theory]
    [AutoData]
    public async Task GetCalendarFromRepository(AddCalendarEvent command)
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
        var handler = new AddCalendarEventHandler(mockRepo.Object);

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
    public async Task SaveCalendarToRepository(AddCalendarEvent command)
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
        var handler = new AddCalendarEventHandler(mockRepo.Object);

        // act
        await handler.Handle(command, default);

        // assert
        mockRepo.Verify(x => x.SaveAsync(
            It.Is<CalendarAggregate>(y => y.Events.First() == command.Event),
            It.IsAny<CancellationToken>()
        ));
    }
}