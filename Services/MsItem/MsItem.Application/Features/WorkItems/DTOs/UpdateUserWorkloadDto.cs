namespace MsItem.Application.Features.WorkItems.DTOs;

/// <summary>
/// Datos enviados a MsUser para actualizar los contadores de carga de trabajo de un usuario
/// tras la asignación o finalización de un ítem de trabajo.
/// </summary>
public sealed record UpdateUserWorkloadDto(
    string Username,
    int IncrementAssigned,
    int IncrementPending,
    int IncrementHighRelevance,
    bool IsCompletion
);
