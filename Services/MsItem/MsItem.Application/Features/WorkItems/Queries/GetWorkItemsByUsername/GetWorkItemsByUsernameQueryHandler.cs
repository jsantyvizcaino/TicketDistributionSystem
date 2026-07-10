using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Queries.GetWorkItemsByUsername;

/// <summary>
/// Maneja la obtención de los ítems de trabajo asignados a un usuario específico.
/// </summary>
public sealed class GetWorkItemsByUsernameQueryHandler : IQueryHandler<GetWorkItemsByUsernameQuery, List<WorkItemResponse>>
{
    private readonly IWorkItemRepository _repository;

    public GetWorkItemsByUsernameQueryHandler(IWorkItemRepository repository) => _repository = repository;

    /// <summary>
    /// Ejecuta la consulta y devuelve los ítems asignados al usuario indicado.
    /// </summary>
    public async ValueTask<List<WorkItemResponse>> Handle(GetWorkItemsByUsernameQuery query, CancellationToken ct)
    {
        var workItems = await _repository.GetByUsernameAsync(query.Username, ct);

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
