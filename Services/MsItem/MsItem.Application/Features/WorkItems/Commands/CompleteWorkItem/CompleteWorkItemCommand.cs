using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Commands.CompleteWorkItem;
public sealed record CompleteWorkItemCommand(
    Guid WorkItemId
) : ICommand<WorkItemResponse>;
