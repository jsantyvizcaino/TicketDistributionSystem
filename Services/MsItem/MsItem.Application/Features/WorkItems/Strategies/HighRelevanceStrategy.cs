using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Entities;
using MsItem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Strategies;
public sealed class HighRelevanceStrategy : IDistributionStrategy
{
    public string Name => "HighRelevance";

    public bool CanApply(WorkItem item) =>
        item.Relevance == RelevanceLevel.High;

    public AvailableUserDto SelectUser(List<AvailableUserDto> candidates) =>
        candidates
            .Where(u => !u.IsSaturated)
            .OrderBy(u => u.PendingItems)
            .ThenBy(u => u.TotalAssignedItems)
            .First();
}
