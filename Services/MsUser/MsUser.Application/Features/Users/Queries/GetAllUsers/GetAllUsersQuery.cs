using Mediator;
using MsUser.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Application.Features.Users.Queries.GetAllUsers;

/// <summary>
/// Consulta que obtiene todos los usuarios registrados en el sistema.
/// </summary>
public sealed record GetAllUsersQuery : IQuery<List<UserResponse>>;