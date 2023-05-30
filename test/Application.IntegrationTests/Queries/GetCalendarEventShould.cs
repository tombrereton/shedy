using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Application.Handlers.Queries.GetCalendarEvent;
using Shedy.Application.IntegrationTests.Fakes;
using Shedy.Domain.Builders;

namespace Shedy.Application.IntegrationTests.Queries;

public class GetCalendarEventShould
{
    private readonly ServiceProvider _services;

    public GetCalendarEventShould()
    {
        _services = new ServiceCollection()
            .AddApplication()
            .AddFakeInfrastructure()
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
        var db = _services.GetRequiredService<FakeDbContext>();
        await db.AddAsync(calendar);
        await db.SaveChangesAsync();
        
        var mediator = _services.GetRequiredService<IMediator>();
        var query = new GetCalendarEvent(calendar.Id, calendarEvent.Id);

        // act
        var result = await mediator.Send(query);

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