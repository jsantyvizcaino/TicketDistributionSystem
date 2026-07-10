using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Strategies;
public sealed class DefaultStrategy : IDistributionStrategy
{
    public string Name => "Default";

    public bool CanApply(WorkItem item) => true;

    public AvailableUserDto SelectUser(List<AvailableUserDto> candidates) =>
        candidates
            .Where(u => !u.IsSaturated)
            .OrderBy(u => u.TotalAssignedItems)
            .First();
}
