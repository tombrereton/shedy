using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Shedy.Core.Builders;
using Shedy.Core.Calendar;
using Shedy.Infrastructure.Database;

#pragma warning disable CS0618

namespace Shedy.Infrastructure.IntegrationTests;

public sealed class PostgreSqlTest : IAsyncLifetime
{
    private readonly TestcontainerDatabase _postgresqlContainer = new ContainerBuilder<PostgreSqlTestcontainer>()
        .WithDatabase(new PostgreSqlTestcontainerConfiguration
        {
            Database = "db",
            Username = "postgres",
            Password = "postgres",
        })
        .Build();

    private readonly ServiceProvider _services;

    public PostgreSqlTest()
    {
        _postgresqlContainer.StartAsync().GetAwaiter().GetResult();
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new KeyValuePair<string, string?>[]
                { new("ConnectionStrings:Database", _postgresqlContainer.ConnectionString) })
            .Build();

        _services = new ServiceCollection()
            .AddInfrastructure(config)
            .BuildServiceProvider();

        var db = _services.GetRequiredService<ShedyDbContext>();
        db.Database.EnsureCreated();
    }


    [Fact]
    public async Task ExecuteCommand()
    {
        var db = _services.GetRequiredService<ShedyDbContext>();
        var calendar = new CalendarBuilder()
            .CreateCalendar()
            .WithNewCalendarId()
            .WithDefaultOpeningHours()
            .Build();
        await db.Calendars.AddAsync(calendar);
        await db.SaveChangesAsync();

    }

    public async Task InitializeAsync()
    {
    }

    public async Task DisposeAsync()
    {
        await _postgresqlContainer.DisposeAsync();
    }
}