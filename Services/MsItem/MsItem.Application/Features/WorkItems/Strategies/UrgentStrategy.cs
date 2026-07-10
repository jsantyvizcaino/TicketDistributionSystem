using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Strategies;
public sealed class UrgentStrategy : IDistributionStrategy
{
    public string Name => "Urgent";

    public bool CanApply(WorkItem item) =>
        (item.DueDate.Date - DateTime.UtcNow.Date).TotalDays < 3;

    public AvailableUserDto SelectUser(List<AvailableUserDto> candidates) =>
        candidates
            .Where(u => !u.IsSaturated)
            .OrderBy(u => u.TotalAssignedItems)
            .First();
}
