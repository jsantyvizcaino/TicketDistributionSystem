using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsItem.Application.Interfaces;
using MsItem.Infrastructure.Http.Providers.Native;
using MsItem.Infrastructure.Http.Providers.Refit;
using System;

namespace MsItem.Infrastructure.Http;

public static class HttpClientProviderFactory
{
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
