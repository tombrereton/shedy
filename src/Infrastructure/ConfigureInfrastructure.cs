using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Interfaces;
using Shedy.Infrastructure.Persistence;
using Shedy.Infrastructure.Repositories;

namespace Shedy.Infrastructure;

public static class ConfigureInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString("Database");
        Guard.Against.NullOrEmpty(dbConnectionString, nameof(dbConnectionString));
        
        services.AddScoped<ICalendarRepository, CalendarRepository>();
        services.AddDbContext<ShedyDbContext>(options =>
        {
            options
                .UseNpgsql(dbConnectionString)
                .UseSnakeCaseNamingConvention();
        });

        return services;
    }
}