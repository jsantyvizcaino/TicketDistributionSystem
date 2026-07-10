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

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}