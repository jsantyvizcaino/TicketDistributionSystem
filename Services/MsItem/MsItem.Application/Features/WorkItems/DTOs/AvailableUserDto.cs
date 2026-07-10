using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.DTOs;
public sealed record AvailableUserDto(
    Guid Id,
    string Username,
    int TotalAssignedItems,
    int CompletedItems,
    int PendingItems,
    int HighRelevanceItems,
    bool IsSaturated
);
