using Mediator;
using MsUser.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Application.Features.Users.Queries.GetUserByUsername;

/// <summary>
/// Consulta que obtiene un usuario activo a partir de su nombre de usuario.
/// </summary>
public sealed record GetUserByUsernameQuery(string Username) : IQuery<UserResponse>;
