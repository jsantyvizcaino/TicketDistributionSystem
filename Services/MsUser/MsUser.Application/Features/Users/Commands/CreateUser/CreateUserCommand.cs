using Mediator;
using MsUser.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Application.Features.Users.Commands.CreateUser;

/// <summary>
/// Comando para crear un nuevo usuario en el sistema.
/// </summary>
public sealed record CreateUserCommand(
    string Username,
    string Email,
    string FullName
) : ICommand<UserResponse>;