using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Strategies;
public interface IDistributionStrategy
{
    string Name { get; }
    bool CanApply(WorkItem item);
    AvailableUserDto SelectUser(List<AvailableUserDto> candidates);
}