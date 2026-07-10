using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Commands.UpdateWorkItem;

/// <summary>
/// Comando para actualizar los datos editables (título, descripción, relevancia y fecha límite)
/// de un ítem de trabajo existente.
/// </summary>
public sealed record UpdateWorkItemCommand(
    Guid WorkItemId,
    string Title,
    string Description,
    RelevanceLevel Relevance,
    DateTime DueDate
) : ICommand<WorkItemResponse>;
