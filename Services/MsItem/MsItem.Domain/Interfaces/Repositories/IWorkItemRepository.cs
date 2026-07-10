using MsItem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Domain.Interfaces.Repositories;

/// <summary>
/// Contrato de acceso a datos específico de la entidad <see cref="WorkItem"/>.
/// </summary>
public interface IWorkItemRepository : IBaseRepository<WorkItem>
{
    /// <summary>
    /// Obtiene todos los ítems de trabajo asignados al usuario indicado.
    /// </summary>
    Task<List<WorkItem>> GetByUsernameAsync(string username, CancellationToken ct = default);

    /// <summary>
    /// Obtiene los ítems de trabajo pendientes de asignación (sin usuario asignado).
    /// </summary>
    Task<List<WorkItem>> GetPendingAsync(CancellationToken ct = default);
}
