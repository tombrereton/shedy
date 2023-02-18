using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Builders;
using Shedy.Core.Calendar;
using Shedy.Core.Handlers.AddAvailability;
using Shedy.Core.Handlers.AddCalendarEvent;
using Shedy.Core.IntegrationTests.Fakes;
using Shedy.Core.Interfaces;

namespace Shedy.Core.IntegrationTests.Features;

public class AddCalendarEventShould
{
    private readonly ServiceProvider _services;

    public AddCalendarEventShould()
    {
        _services = new ServiceCollection()
            .AddCore()
            .AddFakeRepositories()
            .BuildServiceProvider();
    }

    [Theory]
    [AutoData]
    public async Task AddCalendarEventToExistingCalendar(AddCalendarEvent command)
    {
        // arrange
        var mediator = _services.GetRequiredService<IMediator>();
        var repo = _services.GetRequiredService<ICalendarRepository>();
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithCalendarId(command.CalendarId)
            .WithUserId(Guid.NewGuid())
            .WithEmptyOpeningHours()
            .Build();
        await repo.SaveAsync(calendar, default);

        // act
        var result = await mediator.Send(command, default);

        // assert
        result.Event.Should().Be(command.Event);
    }

    [Theory]
    [AutoData]
    public async Task ValidateAddCalendarEvent(CalendarEvent calendarEvent)
    {
        // arrange
        var command = new AddCalendarEvent(Guid.Empty, calendarEvent);
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
        var command = new AddCalendarEvent(Guid.NewGuid(), null!);
        var mediator = _services.GetRequiredService<IMediator>();

        // act
        Func<Task> act = async () => { await mediator.Send(command, default); };

        // assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage("*Event*not*empty*");
    }
}