using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MsUser.Infrastructure.Persistence;
using MsUser.Infrastructure.Persistence.Seed;

namespace MsUser.Infrastructure.Extensions;
/// <summary>
/// Extensiones para operaciones de base de datos
/// </summary>
public static class DatabaseExtensions
{
    /// <summary>
    /// Aplica migraciones y seed data de forma idempotente
    /// </summary>
    public static async Task UseDefaultUsersSeedAsync(this WebApplication app, CancellationToken ct = default)
    {
        using var scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("UsersSeed");
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        logger.LogInformation("Starting users seed.");
        await UserSeedData.EnsureDefaultUsersAsync(context, ct);
        logger.LogInformation("Users seed completed.");
    }
}
