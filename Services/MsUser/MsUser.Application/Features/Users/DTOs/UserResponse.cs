using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Application.Features.Users.DTOs;

/// <summary>
/// Representación de un usuario expuesta por la API de MsUser, incluyendo sus
/// métricas de carga de trabajo y el indicador de saturación.
/// </summary>
public sealed record UserResponse(
    Guid Id,
    string Username,
    string Email,
    string FullName,
    bool IsActive,
    int TotalAssignedItems,
    int CompletedItems,
    int PendingItems,
    int HighRelevanceItems,
    bool IsSaturated
);
