using MsUser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Domain.Interfaces.Repositories;

/// <summary>
/// Contrato de acceso a datos específico de la entidad <see cref="User"/>.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Obtiene un usuario activo por su nombre de usuario, o <c>null</c> si no existe o no está activo.
    /// </summary>
    Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default);

    /// <summary>
    /// Obtiene los usuarios activos y no saturados, elegibles para recibir nuevos ítems de trabajo,
    /// ordenados por menor cantidad de ítems pendientes.
    /// </summary>
    Task<List<User>> GetAvailableForDistributionAsync(CancellationToken ct = default);
}