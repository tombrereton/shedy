using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Aggregates.Calendar;
using Shedy.Core.Builders;
using Shedy.Core.Commands.CreateCalendarEvent;
using Shedy.Core.IntegrationTests.Fakes;

namespace Shedy.Core.IntegrationTests.Commands;

public class CreateCalendarEventShould
{
    private readonly ServiceProvider _services;

    public CreateCalendarEventShould()
    {
        _services = new ServiceCollection()
            .AddCore()
            .AddFakeInfrastructure()
            .BuildServiceProvider();
    }

    [Fact]
    public async Task AddCalendarEventToExistingCalendar()
    {
        // arrange
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithDefaultOpeningHours(TimeZoneInfo.Local)
            .Build();
        var db = _services.GetRequiredService<FakeDbContext>();
        await db.AddAsync(calendar, default);
        await db.SaveChangesAsync(default);

        var calendarEvent = new CalendarEventBuilder()
            .CreateCalendarEvent()
            .WithStart(DateTimeOffset.Parse("2023-01-02T09:00:00Z"))
            .WithDurationInMinutes(30)
            .WithTimeZone(TimeZoneInfo.Local)
            .Build();
        var command = new CreateCalendarEvent(calendar.Id, calendarEvent);
        var mediator = _services.GetRequiredService<IMediator>();

        // act
        var result = await mediator.Send(command, default);

        // assert
        result.Event.Should().Be(command.Event);

        var actual = db.Calendars.First(x => x.Id == calendar.Id);
        actual.Should().BeEquivalentTo(calendar);
    }

    [Theory]
    [AutoData]
    public async Task ValidateAddCalendarEvent(CalendarEvent calendarEvent)
    {
        // arrange
        var command = new CreateCalendarEvent(Guid.Empty, calendarEvent);
        var mediator = _services.GetRequiredService<IMediator>();

        // act
        Func<Task> act = async () => { await mediator.Send(command, default); };

        // assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage("*CalendarId*not*empty*");
    }

    [Fact]
    public async Task ValidateNullCalendarEvent()
    {
        // arrange
        var command = new CreateCalendarEvent(Guid.NewGuid(), null!);
        var mediator = _services.GetRequiredService<IMediator>();

        // act
        Func<Task> act = async () => { await mediator.Send(command, default); };

        // assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage("*Event*not*empty*");
    }
}