using AutoFixture.Xunit2;
using Moq;
using Shedy.Core.Builders;
using Shedy.Core.Calendar;
using Shedy.Core.Handlers.AddCalendarEvent;
using Shedy.Core.UnitTests.Mocks;

namespace Shedy.Core.UnitTests.Handlers;

public class AddCalendarEventHandlerShould
{
    [Fact]
    public async Task GetCalendarFromRepository()
    {
        // act
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithDefaultOpeningHours(TimeZoneInfo.Local)
            .Build();
        var mockRepo = new MockCalendarRepository()
            .CreateGetAsyncStub()
            .WithInputParameters(calendar.Id)
            .WithReturnObject(calendar)
            .Build();
        var calendarEvent = new CalendarEventBuilder()
            .CreateCalendarEvent()
            .WithStart(DateTimeOffset.Parse("2023-01-02T09:00:00Z"))
            .WithDurationInMinutes(30)
            .WithTimeZone(TimeZoneInfo.Local)
            .Build();
        var command = new AddCalendarEvent(calendar.Id, calendarEvent);
        var handler = new AddCalendarEventHandler(mockRepo.Object);

        // act
        await handler.Handle(command, default);

        // assert
        mockRepo.Verify(x => x.GetAsync(
            It.Is<Guid>(y => y == command.CalendarId),
            It.IsAny<CancellationToken>()
        ));
    }
    
    [Fact]
    public async Task SaveCalendarToRepository()
    {
        // act
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithDefaultOpeningHours(TimeZoneInfo.Local)
            .Build();
        var mockRepo = new MockCalendarRepository()
            .CreateGetAsyncStub()
            .WithInputParameters(calendar.Id)
            .WithReturnObject(calendar)
            .Build();
        var calendarEvent = new CalendarEventBuilder()
            .CreateCalendarEvent()
            .WithStart(DateTimeOffset.Parse("2023-01-02T09:00:00Z"))
            .WithDurationInMinutes(30)
            .WithTimeZone(TimeZoneInfo.Local)
            .Build();
        var command = new AddCalendarEvent(calendar.Id, calendarEvent);
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