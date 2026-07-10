using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsItem.Application.Features.WorkItems.Services;
using MsItem.Application.Features.WorkItems.Strategies;
using MsItem.Application.Interfaces;
using MsItem.Domain.Interfaces.Repositories;
using MsItem.Infrastructure.Http;
using MsItem.Infrastructure.Http.Providers.Native;
using MsItem.Infrastructure.Http.Providers.Refit;
using MsItem.Infrastructure.Persistence.Repositories;
using MsItem.Infrastructure.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Infrastructure;

/// <summary>
/// Registro de dependencias de la capa Infrastructure de MsItem.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registra los repositorios, las estrategias de distribución y el cliente HTTP hacia MsUser.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IWorkItemRepository, WorkItemRepository>();

        services.AddDistributionStrategies();
        services.AddUserApiClient(configuration);

        return services;
    }

    /// <summary>
    /// Registra las implementaciones de <see cref="IDistributionStrategy"/> (Urgent, HighRelevance
    /// y Default, en ese orden de precedencia), el <see cref="DistributionContext"/> y el
    /// <see cref="IDistributionService"/>.
    /// </summary>
    private static IServiceCollection AddDistributionStrategies(this IServiceCollection services)
    {
        services.AddScoped<IDistributionStrategy, UrgentStrategy>();
        services.AddScoped<IDistributionStrategy, HighRelevanceStrategy>();
        services.AddScoped<IDistributionStrategy, DefaultStrategy>();
        services.AddScoped<DistributionContext>();
        services.AddScoped<IDistributionService, DistributionService>();

        return services;
    }

    /// <summary>
    /// Registra el cliente HTTP hacia MsUser según el proveedor configurado en
    /// <c>HttpClient:Provider</c> ("refit" o "native"), usando <see cref="HttpClientProviderFactory"/>
    /// para resolver la implementación de <see cref="IUserApiClient"/> en tiempo de ejecución.
    /// </summary>
    private static IServiceCollection AddUserApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        var baseAddress = configuration["Services:MsUser"]
            ?? throw new ArgumentException("La configuración 'Services:MsUser' no está definida");
        var provider = configuration["HttpClient:Provider"];

        switch (provider)
        {
            case "refit":
                services.AddRefitClient<IRefitUserApi>()
                    .ConfigureHttpClient(client => client.BaseAddress = new Uri(baseAddress));
                services.AddScoped<RefitUserApiClient>();
                break;

            case "native":
                services.AddHttpClient<NativeUserApiClient>(client => client.BaseAddress = new Uri(baseAddress));
                break;

            default:
                throw new ArgumentException($"Proveedor de HttpClient '{provider}' no soportado");
        }

        services.AddScoped<IUserApiClient>(sp => HttpClientProviderFactory.Create(configuration, sp));

        return services;
    }
}
