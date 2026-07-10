using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Queries.GetAllWorkItems;

/// <summary>
/// Consulta que obtiene todos los ítems de trabajo registrados.
/// </summary>
public sealed record GetAllWorkItemsQuery : IQuery<List<WorkItemResponse>>;
