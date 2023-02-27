using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shedy.Core.Interfaces;

namespace Shedy.Core.IntegrationTests.Fakes;

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