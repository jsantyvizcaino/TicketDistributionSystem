using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Queries.GetPendingWorkItems;
public sealed class GetPendingWorkItemsQueryHandler : IQueryHandler<GetPendingWorkItemsQuery, List<WorkItemResponse>>
{
    private readonly IWorkItemRepository _repository;

    public GetPendingWorkItemsQueryHandler(IWorkItemRepository repository) => _repository = repository;

    public async ValueTask<List<WorkItemResponse>> Handle(GetPendingWorkItemsQuery query, CancellationToken ct)
    {
        var workItems = await _repository.GetPendingAsync(ct);

        return workItems.Select(w => new WorkItemResponse(
            w.Id,
            w.Title,
            w.Description,
            w.AssignedUsername,
            w.Relevance,
            w.Status,
            w.DueDate,
            w.AssignedAt,
            w.CompletedAt
        )).ToList();
    }
}
