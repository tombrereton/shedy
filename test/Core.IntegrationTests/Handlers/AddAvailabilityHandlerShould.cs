using AutoFixture.Xunit2;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Handlers.AddAvailability;

namespace Shedy.Core.IntegrationTests.Handlers;

public class AddAvailabilityHandlerShould
{
    [Theory]
    [AutoData]
    [Trait("Category", "Integration")]
    public async Task AddAvailabilityToFreelancersCalendar(AddAvailability command)
    {
        // arrange
        var services = new ServiceCollection()
            .AddCoreServices()
            .BuildServiceProvider();

        var handler = services.GetRequiredService<IRequestHandler<AddAvailability, AddAvailabilityResult>>();
        
        // act
        var result = await handler.Handle(command, default);

        // assert
        result.OpeningHours.First().Should().Be(command.Availability);
    }
}