using MsItem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.DTOs;

/// <summary>
/// Representación de un ítem de trabajo expuesta por la API de MsItem.
/// </summary>
public sealed record WorkItemResponse(
    Guid Id,
    string Title,
    string Description,
    string? AssignedUsername,
    RelevanceLevel Relevance,
    WorkItemStatus Status,
    DateTime DueDate,
    DateTime? AssignedAt,
    DateTime? CompletedAt
);
