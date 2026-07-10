using Microsoft.EntityFrameworkCore;
using MsUser.Domain.Entities;

namespace MsUser.Infrastructure.Persistence.Seed;
public static class UserSeedData
{
    public static async Task EnsureDefaultUsersAsync(AppDbContext context, CancellationToken ct = default)
    {
        if (await context.Users.AnyAsync(ct))
            return;

        var users = new List<User>
        {
            new()
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Username = "juan.perez",
                Email = "juan.perez@example.com",
                FullName = "Juan Pérez",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Username = "maria.garcia",
                Email = "maria.garcia@example.com",
                FullName = "María García",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Username = "carlos.lopez",
                Email = "carlos.lopez@example.com",
                FullName = "Carlos López",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.Users.AddRangeAsync(users, ct);
        await context.SaveChangesAsync(ct);
    }
}