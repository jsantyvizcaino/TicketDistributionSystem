using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Domain.Interfaces.Repositories;

/// <summary>
/// Contrato genérico de acceso a datos, común a todas las entidades del dominio.
/// </summary>
/// <typeparam name="T">Tipo de entidad sobre la que opera el repositorio.</typeparam>
public interface IBaseRepository<T> where T : class
{
    /// <summary>
    /// Obtiene una entidad por su identificador, o <c>null</c> si no existe.
    /// </summary>
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Obtiene todas las entidades del tipo <typeparamref name="T"/>.
    /// </summary>
    Task<List<T>> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Agrega una nueva entidad al contexto de persistencia.
    /// </summary>
    Task AddAsync(T entity, CancellationToken ct = default);

    /// <summary>
    /// Marca una entidad existente como modificada.
    /// </summary>
    void Update(T entity);

    /// <summary>
    /// Marca una entidad existente para ser eliminada.
    /// </summary>
    void Delete(T entity);

    /// <summary>
    /// Persiste en la base de datos los cambios pendientes en el contexto.
    /// </summary>
    Task SaveChangesAsync(CancellationToken ct = default);
}
