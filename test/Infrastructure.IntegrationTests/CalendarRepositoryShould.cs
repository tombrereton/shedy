using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Builders;
using Shedy.Core.Interfaces;
using Shedy.Infrastructure.Database;

#pragma warning disable CS0618

namespace Shedy.Infrastructure.IntegrationTests;

public sealed class CalendarRepository : IAsyncLifetime
{
    private readonly TestcontainerDatabase _postgresqlContainer = new ContainerBuilder<PostgreSqlTestcontainer>()
        .WithDatabase(new PostgreSqlTestcontainerConfiguration
        {
            Database = "test",
            Username = "shedy",
            Password = "postgres",
        })
        .Build();

    private ServiceProvider _services = null!;

    [Fact]
    public async Task GetCalendar()
    {
        // arrange
        var db = _services.GetRequiredService<ShedyDbContext>();
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithDefaultOpeningHours()
            .Build();
        await db.Calendars.AddAsync(calendar);
        await db.SaveChangesAsync();

        var repo = _services.GetRequiredService<ICalendarRepository>();

        // act
        var result = await repo.GetAsync(calendar.Id, default);

        // assert
        result.OpeningHours.Should().Equal(calendar.OpeningHours);
        result.UserId.Should().Be(calendar.UserId);
    }

    [Fact]
    public async Task SaveCalendar()
    {
        // arrange
        var db = _services.GetRequiredService<ShedyDbContext>();
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithUserId(Guid.NewGuid())
            .WithDefaultOpeningHours()
            .Build();

        var repo = _services.GetRequiredService<ICalendarRepository>();

        // act
        await repo.SaveAsync(calendar, default);

        // assert
        var result = await db.Calendars.FirstOrDefaultAsync(x => x.Id == calendar.Id);
        result.Should().NotBeNull();
        result!.OpeningHours.Should().Equal(calendar.OpeningHours);
        result.UserId.Should().Be(calendar.UserId);
    }

    public async Task InitializeAsync()
    {
        await _postgresqlContainer.StartAsync();

        var keyValuePairs = new KeyValuePair<string, string?>[]
            { new("ConnectionStrings:Database", _postgresqlContainer.ConnectionString) };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(keyValuePairs)
            .Build();

        _services = new ServiceCollection()
            .AddInfrastructure(config)
            .BuildServiceProvider();

        var db = _services.GetRequiredService<ShedyDbContext>();
        await db.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _postgresqlContainer.DisposeAsync();
    }
}