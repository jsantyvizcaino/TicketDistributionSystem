using Microsoft.EntityFrameworkCore;
using MsUser.Domain.Entities;
using MsUser.Domain.Interfaces.Repositories;

namespace MsUser.Infrastructure.Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default) =>
        await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username && u.IsActive, ct);

    public async Task<List<User>> GetAvailableForDistributionAsync(CancellationToken ct = default) =>
        await _context.Users
            .Where(u => u.IsActive && u.HighRelevanceItems < 3)
            .OrderBy(u => u.PendingItems)
            .ToListAsync(ct);
}