using Mediator;
using MsUser.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Application.Features.Users.Commands.UpdateUserWorkload;
public sealed record UpdateUserWorkloadCommand(
    string Username,
    int IncrementAssigned,
    int IncrementPending,
    int IncrementHighRelevance,
    bool IsCompletion
) : ICommand<UserResponse>;
