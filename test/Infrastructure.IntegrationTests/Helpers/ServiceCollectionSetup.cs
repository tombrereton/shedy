using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Builders;
using Shedy.Core.Interfaces;
using Shedy.Infrastructure.Persistence;

#pragma warning disable CS0618

namespace Shedy.Infrastructure.IntegrationTests.Helpers;

public class ServiceCollectionSetup : IAsyncLifetime
{
    protected ServiceProvider Services = null!;

    private static readonly PostgreSqlTestcontainerConfiguration ContainerConfig = new()
    {
        Database = "test",
        Username = "shedy",
        Password = "postgres",
    };

    private readonly TestcontainerDatabase _postgresqlContainer = new ContainerBuilder<PostgreSqlTestcontainer>()
        .WithDatabase(ContainerConfig)
        .Build();


    public async Task InitializeAsync()
    {
        await _postgresqlContainer.StartAsync();

        var keyValuePairs = new KeyValuePair<string, string?>[]
            { new("ConnectionStrings:Database", _postgresqlContainer.ConnectionString) };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(keyValuePairs)
            .Build();

        Services = new ServiceCollection()
            .AddInfrastructure(config)
            .BuildServiceProvider();

        var db = Services.GetRequiredService<ShedyDbContext>();
        await db.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _postgresqlContainer.DisposeAsync();
    }
}