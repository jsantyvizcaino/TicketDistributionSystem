using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Queries.GetAllWorkItems;
public sealed class GetAllWorkItemsQueryHandler : IQueryHandler<GetAllWorkItemsQuery, List<WorkItemResponse>>
{
    private readonly IWorkItemRepository _repository;

    public GetAllWorkItemsQueryHandler(IWorkItemRepository repository) => _repository = repository;

    public async ValueTask<List<WorkItemResponse>> Handle(GetAllWorkItemsQuery query, CancellationToken ct)
    {
        var workItems = await _repository.GetAllAsync(ct);

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
