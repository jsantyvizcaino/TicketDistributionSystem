using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using System;

namespace MsItem.Application.Features.WorkItems.Commands.DistributeWorkItem;

/// <summary>
/// Comando que dispara la distribución automática de un ítem de trabajo pendiente,
/// asignándolo a un usuario disponible según las estrategias de distribución configuradas.
/// </summary>
public sealed record DistributeWorkItemCommand(
    Guid WorkItemId
) : ICommand<DistributeWorkItemResponse>;
