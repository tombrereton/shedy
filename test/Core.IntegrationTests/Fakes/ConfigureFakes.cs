using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Interfaces;

namespace Shedy.Core.IntegrationTests.Fakes;

public static class ConfigureFakes
{

    public static IServiceCollection AddFakeRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICalendarRepository, FakeCalendarRepository>();
        
        return services;
    }
}