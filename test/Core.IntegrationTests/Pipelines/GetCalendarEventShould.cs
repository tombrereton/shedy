using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Builders;
using Shedy.Core.IntegrationTests.Fakes;
using Shedy.Core.Interfaces;
using Shedy.Core.Queries.GetCalendarEvent;

namespace Shedy.Core.IntegrationTests.Pipelines;

public class GetCalendarEventShould
{
    private readonly ServiceProvider _services;

    public GetCalendarEventShould()
    {
        _services = new ServiceCollection()
            .AddCore()
            .AddFakeRepositories()
            .BuildServiceProvider();
    }

    [Fact]
    public async Task GetCalendarEvent()
    {
        // arrange
        var calendarEvent = new CalendarEventBuilder()
            .CreateCalendarEvent()
            .WithStart(DateTimeOffset.Parse("2023-01-02T09:00:00Z"))
            .WithDurationInMinutes(30)
            .WithTimeZone(TimeZoneInfo.Local)
            .Build();
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithDefaultOpeningHours(TimeZoneInfo.Local)
            .Build();
        calendar.AddEvent(calendarEvent);
        var repo = _services.GetRequiredService<ICalendarRepository>();
        await repo.AddAsync(calendar, default);
        var mediator = _services.GetRequiredService<IMediator>();
        var query = new GetCalendarEvent(calendar.Id, calendarEvent.Id);

        // act
        var result = await mediator.Send(query, default);

        // assert
        result.Event.Should().Be(calendarEvent);
    }

    // [Fact]
    // public async Task ThrowExceptionWhenUserIdNotProvided()
    // {
    //     // arrange
    //     var command = new CreateCalendar(Guid.Empty);
    //     var mediator = _services.GetRequiredService<IMediator>();
    //
    //     // act
    //     Func<Task> act = async () => { await mediator.Send(command, default); };
    //
    //     // assert
    //     await act.Should().ThrowAsync<ValidationException>()
    //         .WithMessage("*UserId*not*empty*");
    // }
}