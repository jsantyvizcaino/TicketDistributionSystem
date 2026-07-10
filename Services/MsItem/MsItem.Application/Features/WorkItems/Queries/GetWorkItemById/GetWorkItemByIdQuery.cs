using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Queries.GetWorkItemById;

/// <summary>
/// Consulta que obtiene un ítem de trabajo por su identificador.
/// </summary>
public sealed record GetWorkItemByIdQuery(Guid Id) : IQuery<WorkItemResponse>;
