using Microsoft.Extensions.DependencyInjection;
using Application.Common.Behaviors;
using FluentValidation;
using MediatR;

namespace Application;

/// <summary>
/// Registers Application-layer services (CQRS handlers, validators, pipeline behaviors).
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register MediatR handlers located in this assembly
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        // Register all FluentValidation validators
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // MediatR pipeline behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // TODO: Add other pipeline behaviors (e.g., LoggingBehavior) when implemented
        return services;
    }
}