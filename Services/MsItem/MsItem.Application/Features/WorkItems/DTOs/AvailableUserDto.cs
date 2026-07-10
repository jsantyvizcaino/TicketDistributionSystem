using System;

namespace MsItem.Application.Features.WorkItems.DTOs;

/// <summary>
/// Representación de un usuario disponible para distribución, obtenida desde MsUser
/// (vía <c>IUserApiClient</c>) para ser evaluada por las estrategias de distribución.
/// </summary>
public sealed record AvailableUserDto(
    Guid Id,
    string Username,
    int TotalAssignedItems,
    int CompletedItems,
    int PendingItems,
    int HighRelevanceItems,
    bool IsSaturated
);
