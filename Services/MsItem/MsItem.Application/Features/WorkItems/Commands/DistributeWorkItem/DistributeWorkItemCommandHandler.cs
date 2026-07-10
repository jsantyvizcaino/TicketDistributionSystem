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
public sealed class DistributeWorkItemCommandHandler : ICommandHandler<DistributeWorkItemCommand, WorkItemResponse>
{
    private readonly IWorkItemRepository _repository;
    private readonly IDistributionService _distributionService;

    public DistributeWorkItemCommandHandler(IWorkItemRepository repository, IDistributionService distributionService)
    {
        _repository = repository;
        _distributionService = distributionService;
    }

    public async ValueTask<WorkItemResponse> Handle(DistributeWorkItemCommand command, CancellationToken ct)
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
