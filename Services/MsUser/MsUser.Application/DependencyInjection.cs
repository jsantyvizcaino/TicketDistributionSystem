using Microsoft.Extensions.DependencyInjection;

namespace MsUser.Application;

/// <summary>
/// Registro de dependencias de la capa Application de MsUser.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registra los servicios de la capa Application, incluyendo Mediator (CQRS).
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator();
        return services;

    }

    /// <summary>
    /// Registra Mediator con ciclo de vida <c>Scoped</c> para el manejo de Commands y Queries.
    /// </summary>
    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        // Mediator (Othamar) — MIT, casi idéntico a MediatR en API
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });
        return services;
    }
}
