using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Interfaces;
using Shedy.Infrastructure.Repositories;

namespace Shedy.Infrastructure;

public static class ConfigureInfrastructure
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICalendarRepository, FakeCalendarRepository>();
        
        return services;
    }
}