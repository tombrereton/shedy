using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Application.Interfaces;

namespace Shedy.Application.IntegrationTests.Fakes;

public static class ConfigureFakes
{

    public static IServiceCollection AddFakeInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICalendarRepository, FakeCalendarRepository>();
        services.AddLogging();
        services.AddDbContext<FakeDbContext>(options =>
            options
                .UseInMemoryDatabase("core_integration_tests")
        );
        
        return services;
    }
}