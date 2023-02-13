using System.Reflection;
using FluentValidation;
using MediatR;
using Shedy.Api.Middleware;
using Shedy.Core.Behaviours;
using Shedy.Core.Handlers.AddAvailability;
using Shedy.Core.Handlers.CreateCalendar;

namespace Shedy.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<ShedyExceptionMiddleware>();

        return services;
    }
}