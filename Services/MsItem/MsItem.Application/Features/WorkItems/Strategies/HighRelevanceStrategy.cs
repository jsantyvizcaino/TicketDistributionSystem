using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Entities;
using MsItem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Strategies;

/// <summary>
/// Estrategia de distribución para ítems de alta relevancia que no son urgentes.
/// Aplica cuando la relevancia del ítem es <see cref="RelevanceLevel.High"/>.
/// Selecciona al usuario no saturado con menor cantidad de ítems pendientes.
/// </summary>
public sealed class HighRelevanceStrategy : IDistributionStrategy
{
    /// <summary>
    /// Nombre identificador de la estrategia, usado para ordenar y depurar el pipeline de distribución.
    /// </summary>
    public string Name => "HighRelevance";

    /// <summary>
    /// Determina si el ítem es de alta relevancia.
    /// </summary>
    public bool CanApply(WorkItem item) =>
        item.Relevance == RelevanceLevel.High;

    /// <summary>
    /// Selecciona, entre los candidatos, al usuario con menor cantidad de ítems pendientes
    /// (y, en caso de empate, con menor cantidad total de ítems asignados).
    /// </summary>
    public AvailableUserDto SelectUser(List<AvailableUserDto> candidates) =>
        candidates
            // Excluir usuarios saturados (>= 3 ítems de alta relevancia) según regla de negocio
            .Where(u => !u.IsSaturated)
            .OrderBy(u => u.PendingItems)
            .ThenBy(u => u.TotalAssignedItems)
            .First();
}
