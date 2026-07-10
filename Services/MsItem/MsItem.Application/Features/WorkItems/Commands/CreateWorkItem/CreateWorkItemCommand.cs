using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Commands.CreateWorkItem;

/// <summary>
/// Comando para crear un nuevo ítem de trabajo sin asignar (queda en estado <c>Pending</c>).
/// </summary>
public sealed record CreateWorkItemCommand(
    string Title,
    string Description,
    RelevanceLevel Relevance,
    DateTime DueDate
) : ICommand<WorkItemResponse>;
