using Moq;
using Shedy.Application.Commands.CreateCalendarEvent;
using Shedy.Core.UnitTests.Mocks;
using Shedy.Domain.Builders;

namespace Shedy.Core.UnitTests.Handlers;

public class CreateCalendarEventHandlerShould
{
    [Fact]
    public async Task GetCalendarFromRepository()
    {
        // arrange
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

        var command = new CreateCalendarEvent(calendar.Id, calendarEvent);
        var handler = new CreateCalendarEventHandler(mockRepo.Object);

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
        var command = new CreateCalendarEvent(calendar.Id, calendarEvent);
        var handler = new CreateCalendarEventHandler(mockRepo.Object);

        // act
        await handler.Handle(command, default);

        // assert
        mockRepo.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()
        ));
    }
}