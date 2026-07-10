using System.Collections.Generic;

namespace MsItem.Application.Features.WorkItems.DTOs;

/// <summary>
/// Resultado de la distribución de un ítem de trabajo.
/// Incluye el ítem recién asignado y la lista ordenada de ítems pendientes/en progreso
/// del usuario al que fue asignado, para reflejar su cola de trabajo actualizada.
/// </summary>
public sealed record DistributeWorkItemResponse(
    WorkItemResponse AssignedItem,
    List<WorkItemResponse> UserSortedPendingItems
);
