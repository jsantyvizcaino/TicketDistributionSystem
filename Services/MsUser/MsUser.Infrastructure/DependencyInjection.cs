using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsUser.Domain.Interfaces.Repositories;
using MsUser.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Infrastructure;

/// <summary>
/// Registro de dependencias de la capa Infrastructure de MsUser.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registra los repositorios y demás servicios de infraestructura de MsUser.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}