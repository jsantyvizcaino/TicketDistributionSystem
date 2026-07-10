using Mediator;
using MsUser.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Application.Features.Users.Commands.CreateUser;
public sealed record CreateUserCommand(
    string Username,
    string Email,
    string FullName
) : ICommand<UserResponse>;