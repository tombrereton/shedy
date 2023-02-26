using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Infrastructure.Persistence;

namespace Shedy.Api.IntegrationTests.Extensions;

public static class ServiceCollectionExtensions
{
    public static void EnsureDbCreated<T>(this IServiceCollection services) where T : DbContext
    {
        var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<ShedyDbContext>();
        dbContext.Database.EnsureCreated();
    }
}