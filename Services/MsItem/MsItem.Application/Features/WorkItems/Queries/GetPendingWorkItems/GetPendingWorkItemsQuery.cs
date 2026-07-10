using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Queries.GetPendingWorkItems;

/// <summary>
/// Consulta que obtiene los ítems de trabajo pendientes de asignación (sin usuario asignado).
/// </summary>
public sealed record GetPendingWorkItemsQuery : IQuery<List<WorkItemResponse>>;
