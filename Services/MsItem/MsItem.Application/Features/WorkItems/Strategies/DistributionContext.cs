using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Entities;

namespace MsItem.Application.Features.WorkItems.Strategies;

/// <summary>
/// Contexto del patrón Strategy: mantiene el conjunto de <see cref="IDistributionStrategy"/>
/// disponibles y decide, para cada ítem de trabajo, cuál de ellas aplicar.
/// </summary>
public sealed class DistributionContext
{
    private readonly List<IDistributionStrategy> _strategies;

    /// <summary>
    /// Construye el contexto ordenando las estrategias recibidas por inyección de dependencias,
    /// dejando siempre <c>DefaultStrategy</c> al final para que actúe como respaldo.
    /// </summary>
    public DistributionContext(IEnumerable<IDistributionStrategy> strategies)
    {
        // Ordenar: Urgent primero, Default siempre al final
        _strategies = strategies
            .OrderBy(s => s.Name == "Default" ? int.MaxValue : 0)
            .ToList();
    }

    /// <summary>
    /// Distribuye un ítem de trabajo: excluye a los usuarios saturados de los candidatos,
    /// selecciona la primera estrategia aplicable al ítem y delega en ella la elección del usuario.
    /// </summary>
    /// <param name="item">Ítem de trabajo a distribuir.</param>
    /// <param name="candidates">Usuarios disponibles obtenidos desde MsUser.</param>
    /// <returns>El usuario seleccionado y el nombre de la estrategia utilizada.</returns>
    public (AvailableUserDto User, string StrategyUsed) Distribute(
        WorkItem item,
        List<AvailableUserDto> candidates)
    {
        // Excluir usuarios saturados (>= 3 ítems de alta relevancia) según regla de negocio,
        // antes de evaluar cualquier estrategia.
        var available = candidates.Where(u => !u.IsSaturated).ToList();

        if (available.Count == 0)
            throw new InvalidOperationException("No hay usuarios disponibles para asignación");

        var strategy = _strategies.FirstOrDefault(s => s.CanApply(item))
            ?? throw new InvalidOperationException("No se encontró estrategia aplicable");

        var selectedUser = strategy.SelectUser(available);
        return (selectedUser, strategy.Name);
    }
}
