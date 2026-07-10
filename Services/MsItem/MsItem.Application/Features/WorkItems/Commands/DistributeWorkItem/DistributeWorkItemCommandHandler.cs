using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Application.Features.WorkItems.Queries.GetSortedPendingByUser;
using MsItem.Application.Features.WorkItems.Services;
using MsItem.Domain.Enums;
using MsItem.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Commands.DistributeWorkItem;

/// <summary>
/// Maneja la distribución de un ítem de trabajo: lo asigna a un usuario disponible
/// mediante <see cref="IDistributionService"/> y, tras persistir la asignación,
/// recalcula y devuelve la cola de pendientes ordenada del usuario asignado.
/// </summary>
public sealed class DistributeWorkItemCommandHandler : ICommandHandler<DistributeWorkItemCommand, DistributeWorkItemResponse>
{
    private readonly IWorkItemRepository _repository;
    private readonly IDistributionService _distributionService;
    private readonly IMediator _mediator;

    public DistributeWorkItemCommandHandler(
        IWorkItemRepository repository,
        IDistributionService distributionService,
        IMediator mediator)
    {
        _repository = repository;
        _distributionService = distributionService;
        _mediator = mediator;
    }

    /// <summary>
    /// Ejecuta el comando: valida que el ítem exista y no esté ya asignado, delega la selección
    /// de usuario en el servicio de distribución, persiste el cambio y, después de guardar,
    /// consulta y ordena (por fecha límite ascendente y relevancia descendente) los ítems
    /// pendientes/en progreso del usuario recién asignado.
    /// </summary>
    public async ValueTask<DistributeWorkItemResponse> Handle(DistributeWorkItemCommand command, CancellationToken ct)
    {
        var workItem = await _repository.GetByIdAsync(command.WorkItemId, ct)
            ?? throw new KeyNotFoundException($"Ítem de trabajo {command.WorkItemId} no encontrado");

        if (workItem.AssignedUsername is not null)
            throw new InvalidOperationException($"El ítem de trabajo {command.WorkItemId} ya está asignado");

        var username = await _distributionService.DistributeAsync(workItem, ct);

        workItem.AssignedUsername = username;
        workItem.Status = WorkItemStatus.InProgress;
        workItem.AssignedAt = DateTime.UtcNow;
        workItem.UpdatedAt = DateTime.UtcNow;

        _repository.Update(workItem);
        await _repository.SaveChangesAsync(ct);

        // Después de persistir la asignación, se recalcula el orden de la cola de trabajo
        // del usuario asignado (fecha límite ascendente, relevancia descendente).
        var sortedPendingItems = await _mediator.Send(new GetSortedPendingByUserQuery(username), ct);

        var assignedItem = new WorkItemResponse(
            workItem.Id,
            workItem.Title,
            workItem.Description,
            workItem.AssignedUsername,
            workItem.Relevance,
            workItem.Status,
            workItem.DueDate,
            workItem.AssignedAt,
            workItem.CompletedAt
        );

        return new DistributeWorkItemResponse(assignedItem, sortedPendingItems);
    }
}
