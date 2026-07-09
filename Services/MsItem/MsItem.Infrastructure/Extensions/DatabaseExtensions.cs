using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MsItem.Infrastructure.Persistence;
using MsItem.Infrastructure.Persistence.Seed;

namespace MsItem.Infrastructure.Extensions;
/// <summary>
/// Extensiones para operaciones de base de datos
/// </summary>
public static class DatabaseExtensions
{
    /// <summary>
    /// Aplica migraciones y seed data de forma idempotente
    /// </summary>
    public static async Task UseDefaultWorkItemsSeedAsync(this WebApplication app, CancellationToken ct = default)
    {
        using var scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("WorkItemsSeed");
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        logger.LogInformation("Starting work items seed.");
        await WorkItemSeedData.EnsureDefaultWorkItemsAsync(context, ct);
        logger.LogInformation("Work items seed completed.");
    }
}
