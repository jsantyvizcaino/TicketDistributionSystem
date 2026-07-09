using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Application.Features.Users.DTOs;
public sealed record UserResponse(
    Guid Id,
    string Username,
    string Email,
    string FullName,
    bool IsActive,
    int TotalAssignedItems,
    int CompletedItems,
    int PendingItems,
    int HighRelevanceItems,
    bool IsSaturated
);
