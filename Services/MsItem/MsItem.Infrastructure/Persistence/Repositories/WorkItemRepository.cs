using Microsoft.EntityFrameworkCore;
using MsItem.Domain.Entities;
using MsItem.Domain.Enums;
using MsItem.Domain.Interfaces.Repositories;

namespace MsItem.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementación de <see cref="IWorkItemRepository"/> basada en Entity Framework Core.
/// </summary>
public class WorkItemRepository : BaseRepository<WorkItem>, IWorkItemRepository
{
    public WorkItemRepository(AppDbContext context) : base(context) { }

    /// <inheritdoc />
    public async Task<List<WorkItem>> GetByUsernameAsync(string username, CancellationToken ct = default) =>
        await _context.WorkItems
            .Where(w => w.AssignedUsername == username)
            .OrderBy(w => w.DueDate)
            .ToListAsync(ct);

    /// <inheritdoc />
    public async Task<List<WorkItem>> GetPendingAsync(CancellationToken ct = default) =>
        await _context.WorkItems
            .Where(w => w.Status == WorkItemStatus.Pending && w.AssignedUsername == null)
            .OrderBy(w => w.DueDate)
            .ToListAsync(ct);
}
