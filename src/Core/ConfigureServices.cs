using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shedy.Core.Behaviours;
using Shedy.Core.Handlers.AddAvailability;
using Shedy.Core.Handlers.CreateCalendar;

namespace Shedy.Core;

public static class ConfigureServices
{

    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(typeof(AddAvailabilityHandler).GetTypeInfo().Assembly);
        services.AddValidatorsFromAssemblyContaining<CreateCalendar>(ServiceLifetime.Transient);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        return services;
    }
}