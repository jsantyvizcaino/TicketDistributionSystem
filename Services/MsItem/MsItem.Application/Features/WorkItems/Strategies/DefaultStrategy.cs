using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Strategies;

/// <summary>
/// Estrategia de distribución de respaldo (fallback).
/// Aplica a cualquier ítem que no haya sido resuelto por una estrategia más específica
/// (Urgent o HighRelevance). Selecciona al usuario no saturado con menor cantidad
/// total de ítems asignados.
/// </summary>
public sealed class DefaultStrategy : IDistributionStrategy
{
    /// <summary>
    /// Nombre identificador de la estrategia, usado para ordenar y depurar el pipeline de distribución.
    /// Se ordena siempre al final del pipeline en <see cref="DistributionContext"/>.
    /// </summary>
    public string Name => "Default";

    /// <summary>
    /// Siempre aplicable: actúa como estrategia de respaldo cuando ninguna otra es apta.
    /// </summary>
    public bool CanApply(WorkItem item) => true;

    /// <summary>
    /// Selecciona, entre los candidatos, al usuario con menor cantidad total de ítems asignados.
    /// </summary>
    public AvailableUserDto SelectUser(List<AvailableUserDto> candidates) =>
        candidates
            // Excluir usuarios saturados (>= 3 ítems de alta relevancia) según regla de negocio
            .Where(u => !u.IsSaturated)
            .OrderBy(u => u.TotalAssignedItems)
            .First();
}
