using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Aggregates.Calendar;
using Shedy.Core.Builders;
using Shedy.Core.Commands.UpdateOpeningTimes;
using Shedy.Core.IntegrationTests.Fakes;
using Shedy.Core.Interfaces;

namespace Shedy.Core.IntegrationTests.Commands;

public class UpdateOpeningTimesShould
{
    private readonly ServiceProvider _services;

    public UpdateOpeningTimesShould()
    {
        _services = new ServiceCollection()
            .AddCore()
            .AddFakeRepositories()
            .BuildServiceProvider();
    }

    [Fact]
    public async Task UpdateOpeningTimesOfExistingCalendar()
    {
        // arrange
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithEmptyOpeningHours()
            .Build();
        var repo = _services.GetRequiredService<ICalendarRepository>();
        await repo.AddAsync(calendar, default);
        var mediator = _services.GetRequiredService<IMediator>();
        var newAvailability = new OpeningTimesBuilder()
            .CreateOpeningTimes()
            .WithDay(DayOfWeek.Monday)
            .WithDefaultStartAndFinishTimes()
            .WithTimeZone(TimeZoneInfo.Local)
            .Build();
        var command = new UpdateOpeningTimes(calendar.Id, newAvailability);

        // act
        var result = await mediator.Send(command, default);

        // assert
        result.OpeningHours.Should().BeEquivalentTo(command.OpeningTimes);
        var actual = await repo.GetAsync(calendar.Id, default);
        calendar.UpdateOpeningTimes(newAvailability);
        actual.Should().BeEquivalentTo(calendar);
    }

    [Theory]
    [AutoData]
    public async Task ValidateEmptyCalendarId(IEnumerable<OpeningTime> availability)
    {
        // arrange
        var command = new UpdateOpeningTimes(Guid.Empty, availability);
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
        var command = new UpdateOpeningTimes(Guid.NewGuid(), null!);
        var mediator = _services.GetRequiredService<IMediator>();

        // act
        Func<Task> act = async () => { await mediator.Send(command, default); };

        // assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage("*OpeningTimes*not*empty*");
    }
}