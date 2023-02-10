using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Npgsql;

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

    [Fact]
    public void ExecuteCommand()
    {
        using (var connection = new NpgsqlConnection(_postgresqlContainer.ConnectionString))
        {
            using (var command = new NpgsqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT 1";
                command.ExecuteReader();
            }
        }
    }

    public Task InitializeAsync()
    {
        return _postgresqlContainer.StartAsync();
    }

    public Task DisposeAsync()
    {
        return _postgresqlContainer.DisposeAsync().AsTask();
    }
}