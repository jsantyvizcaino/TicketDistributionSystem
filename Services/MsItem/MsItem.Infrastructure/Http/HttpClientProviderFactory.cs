using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsItem.Application.Interfaces;
using MsItem.Infrastructure.Http.Providers.Native;
using MsItem.Infrastructure.Http.Providers.Refit;
using System;

namespace MsItem.Infrastructure.Http;

/// <summary>
/// Factory (patrón Factory Method) que resuelve la implementación concreta de
/// <see cref="IUserApiClient"/> a partir del proveedor configurado en <c>HttpClient:Provider</c>.
/// </summary>
public static class HttpClientProviderFactory
{
    /// <summary>
    /// Crea/resuelve el <see cref="IUserApiClient"/> correspondiente al proveedor configurado
    /// ("refit" o "native"). Lanza <see cref="ArgumentException"/> si el valor no es reconocido.
    /// </summary>
    public static IUserApiClient Create(IConfiguration configuration, IServiceProvider serviceProvider)
    {
        var provider = configuration["HttpClient:Provider"];

        return provider switch
        {
            "refit" => serviceProvider.GetRequiredService<RefitUserApiClient>(),
            "native" => serviceProvider.GetRequiredService<NativeUserApiClient>(),
            _ => throw new ArgumentException($"Proveedor de HttpClient '{provider}' no soportado")
        };
    }
}
