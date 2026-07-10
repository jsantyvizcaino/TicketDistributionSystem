using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Strategies;

/// <summary>
/// Estrategia de distribución para ítems urgentes.
/// Aplica cuando la fecha de entrega es menor a 3 días desde la fecha actual.
/// Selecciona al usuario no saturado con menor cantidad total de ítems asignados.
/// </summary>
public sealed class UrgentStrategy : IDistributionStrategy
{
    /// <summary>
    /// Nombre identificador de la estrategia, usado para ordenar y depurar el pipeline de distribución.
    /// </summary>
    public string Name => "Urgent";

    /// <summary>
    /// Determina si el ítem es urgente: su fecha límite está a menos de 3 días de la fecha actual (UTC).
    /// </summary>
    public bool CanApply(WorkItem item) =>
        (item.DueDate.Date - DateTime.UtcNow.Date).TotalDays < 3;

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
