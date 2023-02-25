using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Commands.CreateCalendar;
using Shedy.Core.IntegrationTests.Fakes;
using Shedy.Core.Interfaces;

namespace Shedy.Core.IntegrationTests.Commands;

public class CreateCalendarShould
{
    private readonly ServiceProvider _services;

    public CreateCalendarShould()
    {
        _services = new ServiceCollection()
            .AddCore()
            .AddFakeRepositories()
            .BuildServiceProvider();
    }

    [Theory]
    [AutoData]
    public async Task CreateCalendarWithCorrectUserId(CreateCalendar command)
    {
        // arrange
        var mediator = _services.GetRequiredService<IMediator>();
        var repo = _services.GetRequiredService<ICalendarRepository>();

        // act
        var result = await mediator.Send(command, default);

        // assert
        result.CalendarId.Should().NotBeEmpty();

        var calendar = await repo.GetAsync(result.CalendarId, default);
        calendar.Should().NotBeNull();
        calendar!.Id.Should().Be(result.CalendarId);
    }

    [Fact]
    public async Task ThrowExceptionWhenUserIdNotProvided()
    {
        // arrange
        var command = new CreateCalendar(Guid.Empty);
        var mediator = _services.GetRequiredService<IMediator>();

        // act
        Func<Task> act = async () => { await mediator.Send(command, default); };

        // assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage("*UserId*not*empty*");
    }
}