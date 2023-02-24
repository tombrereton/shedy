using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Aggregates.Calendar;
using Shedy.Core.Builders;
using Shedy.Core.Handlers.CreateOpeningTime;
using Shedy.Core.IntegrationTests.Fakes;
using Shedy.Core.Interfaces;

namespace Shedy.Core.IntegrationTests.Features;

public class CreateOpeningTimeShould
{
    private readonly ServiceProvider _services;

    public CreateOpeningTimeShould()
    {
        _services = new ServiceCollection()
            .AddCore()
            .AddFakeRepositories()
            .BuildServiceProvider();
    }

    [Theory]
    [AutoData]
    public async Task AddAvailabilityExistingCalendar(CreateOpeningTime command)
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
        await repo.AddAsync(calendar, default);
        await repo.SaveChangesAsync(default);

        // act
        var result = await mediator.Send(command, default);

        // assert
        result.OpeningHours.First().Should().Be(command.OpeningTime);
    }

    [Theory]
    [AutoData]
    public async Task ValidateAddAvailability(OpeningTime openingTime)
    {
        // arrange
        var command = new CreateOpeningTime(Guid.Empty, openingTime);
        var mediator = _services.GetRequiredService<IMediator>();

        // act
        Func<Task> act = async () => { await mediator.Send(command, default); };

        // assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage("*CalendarId*not*empty*");
    }
}