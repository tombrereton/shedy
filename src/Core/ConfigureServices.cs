using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Handlers.AddAvailability;

namespace Shedy.Core;

public static class ConfigureServices
{

    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(AddAvailabilityHandler).GetTypeInfo().Assembly);
        
        return services;
    }
}