using AutoFixture.Xunit2;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Builders;
using Shedy.Core.Handlers.AddAvailability;
using Shedy.Core.IntegrationTests.Fakes;
using Shedy.Core.Interfaces;

namespace Shedy.Core.IntegrationTests.Handlers;

public class AddAvailabilityHandlerShould
{
    [Theory]
    [AutoData]
    public async Task AddAvailabilityToFreelancersCalendar(AddAvailability command)
    {
        // arrange
        var services = new ServiceCollection()
            .AddCoreServices()
            .AddFakeRepositories()
            .BuildServiceProvider();

        var handler = services.GetRequiredService<IRequestHandler<AddAvailability, AddAvailabilityResult>>();
        var repo = services.GetRequiredService<ICalendarRepository>();
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithCalendarId(command.CalendarId)
            .WithEmptyOpeningHours()
            .Build();
        await repo.SaveAsync(calendar, default);
        
        // act
        var result = await handler.Handle(command, default);

        // assert
        result.OpeningHours.First().Should().Be(command.Availability);
    }
}