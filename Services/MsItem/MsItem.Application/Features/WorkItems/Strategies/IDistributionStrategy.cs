using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Strategies;

/// <summary>
/// Contrato del patrón Strategy para la distribución de ítems de trabajo.
/// Cada implementación define cuándo es aplicable (<see cref="CanApply"/>) y cómo
/// selecciona al usuario destinatario entre los candidatos disponibles (<see cref="SelectUser"/>).
/// </summary>
public interface IDistributionStrategy
{
    /// <summary>
    /// Nombre identificador de la estrategia.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Determina si esta estrategia es aplicable al ítem de trabajo dado.
    /// </summary>
    bool CanApply(WorkItem item);

    /// <summary>
    /// Selecciona al usuario destinatario del ítem entre la lista de candidatos disponibles.
    /// </summary>
    AvailableUserDto SelectUser(List<AvailableUserDto> candidates);
}
