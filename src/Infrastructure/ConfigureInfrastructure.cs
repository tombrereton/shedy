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
        services.AddScoped<ICalendarRepository, CalendarRepository>();

        services.AddDbContext<ShedyDbContext>(options =>
            options
                .UseNpgsql(configuration.GetConnectionString("Database"))
                .UseSnakeCaseNamingConvention()
        );

        return services;
    }
}