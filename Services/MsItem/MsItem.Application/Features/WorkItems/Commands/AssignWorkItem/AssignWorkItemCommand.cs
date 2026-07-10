using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Commands.AssignWorkItem;

/// <summary>
/// Comando para asignar manualmente un ítem de trabajo a un usuario específico.
/// </summary>
public sealed record AssignWorkItemCommand(
    Guid WorkItemId,
    string Username
) : ICommand<WorkItemResponse>;
