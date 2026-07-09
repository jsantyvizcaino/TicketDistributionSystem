using Microsoft.EntityFrameworkCore;
using MsUser.Domain.Interfaces.Repositories;

namespace MsUser.Infrastructure.Persistence.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    protected BaseRepository(AppDbContext context) => _context = context;

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _context.Set<T>().FindAsync([id], ct);

    public async Task<List<T>> GetAllAsync(CancellationToken ct = default) =>
        await _context.Set<T>().ToListAsync(ct);

    public async Task AddAsync(T entity, CancellationToken ct = default) =>
        await _context.Set<T>().AddAsync(entity, ct);

    public void Update(T entity) =>
        _context.Set<T>().Update(entity);

    public void Delete(T entity) =>
        _context.Set<T>().Remove(entity);

    public async Task SaveChangesAsync(CancellationToken ct = default) =>
        await _context.SaveChangesAsync(ct);
}