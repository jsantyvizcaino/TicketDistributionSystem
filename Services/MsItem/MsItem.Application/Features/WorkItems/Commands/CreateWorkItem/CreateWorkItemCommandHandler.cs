using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Entities;
using MsItem.Domain.Enums;
using MsItem.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Commands.CreateWorkItem;
public sealed class CreateWorkItemCommandHandler : ICommandHandler<CreateWorkItemCommand, WorkItemResponse>
{
    private readonly IWorkItemRepository _repository;

    public CreateWorkItemCommandHandler(IWorkItemRepository repository) => _repository = repository;

    public async ValueTask<WorkItemResponse> Handle(CreateWorkItemCommand command, CancellationToken ct)
    {
        var workItem = new WorkItem
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
            AssignedUsername = null,
            Relevance = command.Relevance,
            Status = WorkItemStatus.Pending,
            DueDate = command.DueDate,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(workItem, ct);
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
