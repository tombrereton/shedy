using System.Net;
using System.Net.Http.Json;
using AutoFixture.Xunit2;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using FluentAssertions;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Api.IntegrationTests.Extensions;
using Shedy.Api.IntegrationTests.Helpers;
using Shedy.Api.Requests;
using Shedy.Core.Aggregates.Calendar;
using Shedy.Core.Builders;
using Shedy.Infrastructure.Persistence;
using Xunit.Abstractions;

namespace Shedy.Api.IntegrationTests.Controllers;

public class CalendarControllerShould : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly WebApplicationFactory<Program> _factory;

    private static readonly PostgreSqlTestcontainerConfiguration ContainerConfig = new()
    {
        Database = "test",
        Username = "shedy",
        Password = "postgres",
    };

    private readonly TestcontainerDatabase _dbContainer = new ContainerBuilder<PostgreSqlTestcontainer>()
        .WithDatabase(ContainerConfig)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseConfiguration(CreateConfig());
        builder.ConfigureTestServices(services => services.EnsureDbCreated<ShedyDbContext>());
        builder.UseEnvironment("Development");
    }

    private IConfigurationRoot CreateConfig()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddInMemoryCollection(CreateInMemoryConfig())
            .Build();
        return config;
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }

    private KeyValuePair<string, string?>[] CreateInMemoryConfig()
    {
        return new KeyValuePair<string, string?>[]
            { new("ConnectionStrings:Database", _dbContainer.ConnectionString) };
    }

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

    [Fact]
    public async Task CreateCalendarEvent()
    {
        // arrange
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithDefaultOpeningHours(TimeZoneInfo.Local)
            .Build();
        var calendarEvent = new CalendarEventBuilder()
            .CreateCalendarEvent()
            .WithStart(DateTimeOffset.Parse("2023-01-02T09:00:00Z"))
            .WithDurationInMinutes(30)
            .WithTimeZone(TimeZoneInfo.Local)
            .Build();
        var request = new CreateCalendarEventRequest(calendarEvent.Adapt<CalendarEventModel>());
        await AddCalendarToDbAsync(calendar);
        var client = _factory.CreateDefaultClient();
        var uri = $"api/calendars/{calendar.Id}/events";

        // act
        var result = await client.PostAsJsonAsync(uri, request);

        // assert
        result.EnsureSuccessStatusCode();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Headers.Should().ContainKey("Location");
        result.Headers.GetValues("Location").Should().ContainMatch("*api/events/*");

        // var updatedCalendar = dbContext.Calendars.FirstOrDefault(x => x.Id == calendar.Id);
        // updatedCalendar.Should().NotBeNull();
    }

    private async Task AddCalendarToDbAsync(CalendarAggregate calendar)
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ShedyDbContext>();
        await dbContext.Calendars.AddAsync(calendar);
        await dbContext.SaveChangesAsync();
        scope.Dispose();
    }
}