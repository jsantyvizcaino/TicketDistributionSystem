using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Enums;
using MsItem.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Queries.GetSortedPendingByUser;

/// <summary>
/// Maneja la obtención y ordenamiento de la cola de trabajo de un usuario.
/// </summary>
public sealed class GetSortedPendingByUserQueryHandler : IQueryHandler<GetSortedPendingByUserQuery, List<WorkItemResponse>>
{
    private readonly IWorkItemRepository _repository;

    public GetSortedPendingByUserQueryHandler(IWorkItemRepository repository) => _repository = repository;

    /// <summary>
    /// Ejecuta la consulta: obtiene los ítems del usuario en estado <c>Pending</c> o <c>InProgress</c>
    /// y los ordena por fecha límite ascendente y, en caso de empate, por relevancia descendente
    /// (los ítems de alta relevancia van primero).
    /// </summary>
    public async ValueTask<List<WorkItemResponse>> Handle(GetSortedPendingByUserQuery query, CancellationToken ct)
    {
        var items = await _repository.GetByUsernameAsync(query.Username, ct);

        return items
            .Where(i => i.Status == WorkItemStatus.Pending || i.Status == WorkItemStatus.InProgress)
            .OrderBy(i => i.DueDate)
            .ThenByDescending(i => i.Relevance)
            .Select(i => new WorkItemResponse(
                i.Id,
                i.Title,
                i.Description,
                i.AssignedUsername,
                i.Relevance,
                i.Status,
                i.DueDate,
                i.AssignedAt,
                i.CompletedAt
            ))
            .ToList();
    }
}