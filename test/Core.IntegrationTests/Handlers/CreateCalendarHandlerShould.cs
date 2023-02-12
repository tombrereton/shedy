using AutoFixture.Xunit2;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Builders;
using Shedy.Core.Handlers.AddAvailability;
using Shedy.Core.Handlers.CreateCalendar;
using Shedy.Core.IntegrationTests.Fakes;
using Shedy.Core.Interfaces;

namespace Shedy.Core.IntegrationTests.Handlers;

public class CreateCalendarHandlerShould
{
    [Theory]
    [AutoData]
    [Trait("Category", "Integration")]
    public async Task CreateCalendarWithCorrectUserId(CreateCalendar command)
    {
        // arrange
        var services = new ServiceCollection()
            .AddCoreServices()
            .AddFakeRepositories()
            .BuildServiceProvider();

        var handler = services.GetRequiredService<IRequestHandler<CreateCalendar, CreateCalendarResult>>();
        var repo = services.GetRequiredService<ICalendarRepository>();
        
        // act
        var result = await handler.Handle(command, default);

        // assert
        result.CalendarId.Should().NotBeEmpty();
    }
}