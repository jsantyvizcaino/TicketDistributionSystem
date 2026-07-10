using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Enums;
using MsItem.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Commands.AssignWorkItem;

/// <summary>
/// Maneja la asignación manual de un ítem de trabajo a un usuario.
/// </summary>
public sealed class AssignWorkItemCommandHandler : ICommandHandler<AssignWorkItemCommand, WorkItemResponse>
{
    private readonly IWorkItemRepository _repository;

    public AssignWorkItemCommandHandler(IWorkItemRepository repository) => _repository = repository;

    /// <summary>
    /// Ejecuta el comando: busca el ítem, lo asigna al usuario indicado y lo marca como <c>InProgress</c>.
    /// </summary>
    public async ValueTask<WorkItemResponse> Handle(AssignWorkItemCommand command, CancellationToken ct)
    {
        var workItem = await _repository.GetByIdAsync(command.WorkItemId, ct)
            ?? throw new KeyNotFoundException($"Ítem de trabajo {command.WorkItemId} no encontrado");

        workItem.AssignedUsername = command.Username;
        workItem.Status = WorkItemStatus.InProgress;
        workItem.AssignedAt = DateTime.UtcNow;
        workItem.UpdatedAt = DateTime.UtcNow;

        _repository.Update(workItem);
        await _repository.SaveChangesAsync(ct);

        return new WorkItemResponse(
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
    }
}
