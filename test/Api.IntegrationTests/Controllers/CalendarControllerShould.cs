using System.Net;
using System.Net.Http.Json;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Api.IntegrationTests.Helpers;
using Shedy.Api.Requests;
using Shedy.Core.Builders;
using Shedy.Infrastructure.Persistence;
using Xunit.Abstractions;

namespace Shedy.Api.IntegrationTests.Controllers;

public class CalendarControllerShould : IClassFixture<ShedyApiFactory>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CalendarControllerShould(ShedyApiFactory factory, ITestOutputHelper output)
    {
        _factory = factory.WithTestLogging(output);
    }

    [Theory]
    [AutoData]
    public async Task CreateCalendar(CreateCalendarRequest request)
    {
        // arrange
        var client = _factory.CreateDefaultClient();
        var uri = "api/calendars";

        // act
        var result = await client.PostAsJsonAsync(uri, request);

        // assert
        result.EnsureSuccessStatusCode();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Headers.Should().ContainKey("Location");
        result.Headers.GetValues("Location").Should().ContainMatch("*api/Calendars/*");

        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ShedyDbContext>();
        var updatedCalendar = dbContext.Calendars.FirstOrDefault(x => x.UserId == request.UserId);
        updatedCalendar.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCalendar()
    {
        // arrange
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithDefaultOpeningHours(TimeZoneInfo.Local)
            .Build();
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ShedyDbContext>();
        await dbContext.Calendars.AddAsync(calendar);
        await dbContext.SaveChangesAsync();
        var client = _factory.CreateDefaultClient();
        var uri = $"api/calendars/{calendar.Id}";

        // act
        var result = await client.GetAsync(uri);

        // assert
        result.EnsureSuccessStatusCode();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Theory]
    [AutoData]
    public async Task CreateCalendarEvent(CreateCalendarEventRequest request)
    {
        // arrange
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithDefaultOpeningHours(TimeZoneInfo.Local)
            .Build();
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ShedyDbContext>();
        await dbContext.Calendars.AddAsync(calendar);
        await dbContext.SaveChangesAsync();
        var client = _factory.CreateDefaultClient();
        var uri = $"api/calendars/{calendar.Id}/events";

        // act
        var result = await client.PostAsJsonAsync(uri, request);

        // assert
        result.EnsureSuccessStatusCode();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Headers.Should().ContainKey("Location");
        result.Headers.GetValues("Location").Should().ContainMatch("*api/events/*");

        var updatedCalendar = dbContext.Calendars.FirstOrDefault(x => x.Id == calendar.Id);
        updatedCalendar.Should().NotBeNull();
    }
}