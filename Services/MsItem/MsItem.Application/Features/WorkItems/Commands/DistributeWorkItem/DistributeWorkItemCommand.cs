using Mediator;
using MsItem.Application.Features.WorkItems.DTOs;
using System;

namespace MsItem.Application.Features.WorkItems.Commands.DistributeWorkItem;
public sealed record DistributeWorkItemCommand(
    Guid WorkItemId
) : ICommand<WorkItemResponse>;
