using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Enums;
using MsItem.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Commands.CompleteWorkItem;
public sealed class CompleteWorkItemCommandHandler : ICommandHandler<CompleteWorkItemCommand, WorkItemResponse>
{
    private readonly IWorkItemRepository _repository;

    public CompleteWorkItemCommandHandler(IWorkItemRepository repository) => _repository = repository;

    public async ValueTask<WorkItemResponse> Handle(CompleteWorkItemCommand command, CancellationToken ct)
    {
        var workItem = await _repository.GetByIdAsync(command.WorkItemId, ct)
            ?? throw new KeyNotFoundException($"Ítem de trabajo {command.WorkItemId} no encontrado");

        workItem.Status = WorkItemStatus.Completed;
        workItem.CompletedAt = DateTime.UtcNow;
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
