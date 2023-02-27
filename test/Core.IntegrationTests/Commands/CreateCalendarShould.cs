using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Commands.CreateCalendar;
using Shedy.Core.IntegrationTests.Fakes;

namespace Shedy.Core.IntegrationTests.Commands;

public class CreateCalendarShould
{
    private readonly ServiceProvider _services;

    public CreateCalendarShould()
    {
        _services = new ServiceCollection()
            .AddCore()
            .AddFakeInfrastructure()
            .BuildServiceProvider();
    }

    [Theory]
    [AutoData]
    public async Task CreateCalendarWithCorrectUserId(CreateCalendar command)
    {
        // arrange
        var mediator = _services.GetRequiredService<IMediator>();
        var db = _services.GetRequiredService<FakeDbContext>();

        // act
        var result = await mediator.Send(command, default);

        // assert
        result.CalendarId.Should().NotBeEmpty();

        var actual = await db.Calendars.FirstOrDefaultAsync(x => x.UserId == command.UserId);
        actual.Should().NotBeNull();
        actual!.Id.Should().Be(result.CalendarId);
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