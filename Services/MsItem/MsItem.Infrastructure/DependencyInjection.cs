using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MsItem.Domain.Interfaces.Repositories;
using MsItem.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IWorkItemRepository, WorkItemRepository>();

        return services;
    }
}
