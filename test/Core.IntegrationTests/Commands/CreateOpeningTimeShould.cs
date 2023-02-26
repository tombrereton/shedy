using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Aggregates.Calendar;
using Shedy.Core.Builders;
using Shedy.Core.Commands.CreateOpeningTime;
using Shedy.Core.IntegrationTests.Fakes;
using Shedy.Core.Interfaces;

namespace Shedy.Core.IntegrationTests.Commands;

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
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithCalendarId(command.CalendarId)
            .WithUserId(Guid.NewGuid())
            .WithEmptyOpeningHours()
            .Build();
        var db = _services.GetRequiredService<FakeDbContext>();
        await db.AddAsync(calendar, default);
        await db.SaveChangesAsync(default);
        var mediator = _services.GetRequiredService<IMediator>();

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