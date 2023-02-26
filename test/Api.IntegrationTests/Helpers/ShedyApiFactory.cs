using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Shedy.Infrastructure.Persistence;

#pragma warning disable CS0618

namespace Shedy.Api.IntegrationTests.Helpers;

public class ShedyApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
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
        builder.ConfigureAppConfiguration(config => config.AddInMemoryCollection(CreateInMemoryConfig()));
        builder.ConfigureTestServices(services => services.EnsureDbCreated<ShedyDbContext>());
        builder.UseEnvironment("Development");
    }

    private KeyValuePair<string, string?>[] CreateInMemoryConfig()
    {
        return new KeyValuePair<string, string?>[]
            { new("ConnectionStrings:Database", _dbContainer.ConnectionString) };
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}