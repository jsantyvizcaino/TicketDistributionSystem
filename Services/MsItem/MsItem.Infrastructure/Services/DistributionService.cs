using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Application.Features.WorkItems.Services;
using MsItem.Application.Features.WorkItems.Strategies;
using MsItem.Application.Interfaces;
using MsItem.Domain.Entities;
using MsItem.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace MsItem.Infrastructure.Services;

/// <summary>
/// Orquesta la distribución de un ítem de trabajo hacia un usuario de MsUser.
/// Combina la información de usuarios disponibles (obtenida vía <see cref="IUserApiClient"/>)
/// con las estrategias de asignación (<see cref="DistributionContext"/>) y notifica
/// el incremento de carga resultante al microservicio de usuarios.
/// </summary>
public sealed class DistributionService : IDistributionService
{
    private readonly IUserApiClient _userApiClient;
    private readonly DistributionContext _distributionContext;

    public DistributionService(IUserApiClient userApiClient, DistributionContext distributionContext)
    {
        _userApiClient = userApiClient;
        _distributionContext = distributionContext;
    }

    /// <summary>
    /// Distribuye un ítem de trabajo aplicando el siguiente flujo:
    /// <para>
    /// Paso 1: Obtener usuarios disponibles desde MsUser (vía <see cref="IUserApiClient.GetAvailableUsersAsync"/>).
    /// </para>
    /// <para>
    /// Paso 2: Filtrar usuarios saturados (aquellos con 3 o más ítems de alta relevancia asignados);
    /// este filtrado ocurre dentro de <see cref="DistributionContext.Distribute"/> y de cada estrategia,
    /// que excluyen explícitamente a los usuarios saturados antes de seleccionar candidato.
    /// </para>
    /// <para>
    /// Paso 3: Evaluar la estrategia aplicable según el tipo de ítem (<see cref="DistributionContext"/>
    /// prueba las estrategias en orden, dejando <c>DefaultStrategy</c> siempre al final como respaldo):
    /// </para>
    /// <para>
    /// Paso 3a: Si la fecha límite es menor a 3 días desde hoy → se aplica <c>UrgentStrategy</c>,
    /// que selecciona al usuario no saturado con menor cantidad total de ítems asignados.
    /// </para>
    /// <para>
    /// Paso 3b: Si el ítem es de relevancia alta → se aplica <c>HighRelevanceStrategy</c>,
    /// que selecciona al usuario no saturado con menor cantidad de ítems pendientes.
    /// </para>
    /// <para>
    /// Paso 3c: Si el ítem es de relevancia baja (y no es urgente) → se aplica <c>DefaultStrategy</c>,
    /// que selecciona al usuario no saturado con menor cantidad total de ítems asignados.
    /// </para>
    /// <para>
    /// Paso 4: Actualizar el workload del usuario seleccionado en MsUser
    /// (vía <see cref="IUserApiClient.UpdateUserWorkloadAsync"/>), incrementando sus contadores
    /// de ítems asignados, pendientes y, si corresponde, de alta relevancia.
    /// </para>
    /// </summary>
    /// <param name="item">Ítem de trabajo a distribuir.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <returns>El nombre de usuario (<c>Username</c>) al que se asignó el ítem.</returns>
    public async Task<string> DistributeAsync(WorkItem item, CancellationToken ct = default)
    {
        // Paso 1: Obtener usuarios disponibles desde MsUser.
        var candidates = await _userApiClient.GetAvailableUsersAsync(ct);

        // Paso 2 y 3: DistributionContext filtra usuarios saturados y aplica
        // la estrategia correspondiente (Urgent / HighRelevance / Default) según el ítem.
        var (user, _) = _distributionContext.Distribute(item, candidates);

        // Paso 4: Actualizar el workload del usuario seleccionado en MsUser.
        var workload = new UpdateUserWorkloadDto(
            user.Username,
            IncrementAssigned: 1,
            IncrementPending: 1,
            IncrementHighRelevance: item.Relevance == RelevanceLevel.High ? 1 : 0,
            IsCompletion: false
        );

        await _userApiClient.UpdateUserWorkloadAsync(user.Username, workload, ct);

        return user.Username;
    }
}
