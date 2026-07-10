using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Entities;

namespace MsItem.Application.Features.WorkItems.Strategies;

public sealed class DistributionContext
{
    private readonly List<IDistributionStrategy> _strategies;

    public DistributionContext(IEnumerable<IDistributionStrategy> strategies)
    {
        // Ordenar: Urgent primero, Default siempre al final
        _strategies = strategies
            .OrderBy(s => s.Name == "Default" ? int.MaxValue : 0)
            .ToList();
    }

    public (AvailableUserDto User, string StrategyUsed) Distribute(
        WorkItem item,
        List<AvailableUserDto> candidates)
    {
        var available = candidates.Where(u => !u.IsSaturated).ToList();

        if (available.Count == 0)
            throw new InvalidOperationException("No hay usuarios disponibles para asignación");

        var strategy = _strategies.FirstOrDefault(s => s.CanApply(item))
            ?? throw new InvalidOperationException("No se encontró estrategia aplicable");

        var selectedUser = strategy.SelectUser(available);
        return (selectedUser, strategy.Name);
    }
}