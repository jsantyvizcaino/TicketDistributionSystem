using Microsoft.EntityFrameworkCore;
using MsUser.Domain.Interfaces.Repositories;

namespace MsUser.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementación genérica de <see cref="IBaseRepository{T}"/> basada en Entity Framework Core.
/// </summary>
/// <typeparam name="T">Tipo de entidad sobre la que opera el repositorio.</typeparam>
public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    protected BaseRepository(AppDbContext context) => _context = context;

    /// <inheritdoc />
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _context.Set<T>().FindAsync([id], ct);

    /// <inheritdoc />
    public async Task<List<T>> GetAllAsync(CancellationToken ct = default) =>
        await _context.Set<T>().ToListAsync(ct);

    /// <inheritdoc />
    public async Task AddAsync(T entity, CancellationToken ct = default) =>
        await _context.Set<T>().AddAsync(entity, ct);

    /// <inheritdoc />
    public void Update(T entity) =>
        _context.Set<T>().Update(entity);

    /// <inheritdoc />
    public void Delete(T entity) =>
        _context.Set<T>().Remove(entity);

    /// <inheritdoc />
    public async Task SaveChangesAsync(CancellationToken ct = default) =>
        await _context.SaveChangesAsync(ct);
}