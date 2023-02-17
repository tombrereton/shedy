using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Builders;
using Shedy.Core.Calendar;
using Shedy.Core.Handlers.AddAvailability;
using Shedy.Core.Handlers.UpdateAvailability;
using Shedy.Core.IntegrationTests.Fakes;
using Shedy.Core.Interfaces;

namespace Shedy.Core.IntegrationTests.Features;

public class UpdateAvailabilityShould
{
    private readonly ServiceProvider _services;

    public UpdateAvailabilityShould()
    {
        _services = new ServiceCollection()
            .AddCore()
            .AddFakeRepositories()
            .BuildServiceProvider();
    }

    [Fact]
    public async Task UpdateAvailabilityOfExistingCalendar()
    {
        // arrange
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithEmptyOpeningHours()
            .Build();
        var repo = _services.GetRequiredService<ICalendarRepository>();
        await repo.SaveAsync(calendar, default);
        var mediator = _services.GetRequiredService<IMediator>();
        var newAvailability = new OpeningHoursBuilder()
            .CreateOpeningHours()
            .WithDay(DayOfWeek.Monday)
            .WithDefaultStartAndFinishTimes()
            .WithTimeZone(TimeZoneInfo.Local)
            .Build();
        var command = new UpdateAvailability(calendar.Id, newAvailability);

        // act
        var result = await mediator.Send(command, default);

        // assert
        result.OpeningHours.Should().BeEquivalentTo(command.Availability);
    }

    [Theory]
    [AutoData]
    public async Task ValidateEmptyCalendarId(IEnumerable<Availability> availability)
    {
        // arrange
        var command = new UpdateAvailability(Guid.Empty, availability);
        var mediator = _services.GetRequiredService<IMediator>();

        // act
        Func<Task> act = async () => { await mediator.Send(command, default); };

        // assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage("*CalendarId*not*empty*");
    }
    
    [Fact]
    public async Task ValidateNullAvailability()
    {
        // arrange
        var command = new UpdateAvailability(Guid.NewGuid(), null!);
        var mediator = _services.GetRequiredService<IMediator>();

        // act
        Func<Task> act = async () => { await mediator.Send(command, default); };

        // assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage("*Availability*not*empty*");
    }
}