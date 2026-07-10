using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Queries.GetWorkItemById;

/// <summary>
/// Maneja la búsqueda de un ítem de trabajo por identificador.
/// </summary>
public sealed class GetWorkItemByIdQueryHandler : IQueryHandler<GetWorkItemByIdQuery, WorkItemResponse>
{
    private readonly IWorkItemRepository _repository;

    public GetWorkItemByIdQueryHandler(IWorkItemRepository repository) => _repository = repository;

    /// <summary>
    /// Ejecuta la consulta: busca el ítem por Id o lanza <see cref="KeyNotFoundException"/> si no existe.
    /// </summary>
    public async ValueTask<WorkItemResponse> Handle(GetWorkItemByIdQuery query, CancellationToken ct)
    {
        var workItem = await _repository.GetByIdAsync(query.Id, ct)
            ?? throw new KeyNotFoundException($"Ítem de trabajo {query.Id} no encontrado");

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
