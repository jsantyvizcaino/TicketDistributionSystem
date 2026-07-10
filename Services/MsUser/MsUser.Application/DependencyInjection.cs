using Microsoft.Extensions.DependencyInjection;

namespace MsUser.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator();
        return services;

    }

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
