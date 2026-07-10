using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Queries.GetWorkItemsByUsername;

/// <summary>
/// Consulta que obtiene todos los ítems de trabajo asignados a un usuario específico.
/// </summary>
public sealed record GetWorkItemsByUsernameQuery(string Username) : IQuery<List<WorkItemResponse>>;
