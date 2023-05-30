using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Application.Commands.CreateOpeningTime;
using Shedy.Application.IntegrationTests.Fakes;
using Shedy.Domain.Aggregates.Calendar;
using Shedy.Domain.Builders;

namespace Shedy.Application.IntegrationTests.Commands;

public class CreateOpeningTimeShould
{
    private readonly ServiceProvider _services;

    public CreateOpeningTimeShould()
    {
        _services = new ServiceCollection()
            .AddApplication()
            .AddFakeInfrastructure()
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
        var actual = db.Calendars.First(x => x.Id == calendar.Id);
        actual.Should().BeEquivalentTo(calendar);
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