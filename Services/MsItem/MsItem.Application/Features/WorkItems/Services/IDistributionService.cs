using MsItem.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Services;

/// <summary>
/// Contrato del servicio que orquesta la distribución de un ítem de trabajo hacia un usuario disponible.
/// </summary>
public interface IDistributionService
{
    /// <summary>
    /// Distribuye el ítem de trabajo dado, seleccionando un usuario según las estrategias
    /// de distribución configuradas y notificando el incremento de carga a MsUser.
    /// </summary>
    /// <returns>El nombre de usuario al que se asignó el ítem.</returns>
    Task<string> DistributeAsync(WorkItem item, CancellationToken ct = default);
}
