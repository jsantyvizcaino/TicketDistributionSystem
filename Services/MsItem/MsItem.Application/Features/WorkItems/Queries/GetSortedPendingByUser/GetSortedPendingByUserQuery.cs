using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using System;

namespace MsItem.Application.Features.WorkItems.Queries.GetSortedPendingByUser;

/// <summary>
/// Consulta que obtiene la cola de ítems pendientes/en progreso de un usuario,
/// ordenada por fecha límite ascendente y relevancia descendente.
/// </summary>
public sealed record GetSortedPendingByUserQuery(string Username) : IQuery<List<WorkItemResponse>>;
