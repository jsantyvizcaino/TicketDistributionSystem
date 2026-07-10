using MsItem.Application.Features.WorkItems.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MsItem.Application.Interfaces;

/// <summary>
/// Contrato de comunicación con el microservicio MsUser.
/// Abstrae la tecnología HTTP concreta (Refit o <c>HttpClient</c> nativo) usada en Infrastructure.
/// </summary>
public interface IUserApiClient
{
    /// <summary>
    /// Obtiene los usuarios activos y no saturados, disponibles para recibir ítems de trabajo.
    /// </summary>
    Task<List<AvailableUserDto>> GetAvailableUsersAsync(CancellationToken ct = default);

    /// <summary>
    /// Actualiza los contadores de carga de trabajo de un usuario en MsUser.
    /// </summary>
    Task UpdateUserWorkloadAsync(string username, UpdateUserWorkloadDto dto, CancellationToken ct = default);
}
