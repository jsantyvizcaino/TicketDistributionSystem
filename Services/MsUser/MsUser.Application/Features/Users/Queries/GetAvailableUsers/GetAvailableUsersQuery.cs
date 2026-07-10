using Mediator;
using MsUser.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Application.Features.Users.Queries.GetAvailableUsers;

/// <summary>
/// Consulta que obtiene los usuarios activos y no saturados, elegibles para
/// recibir nuevos ítems de trabajo en el proceso de distribución.
/// </summary>
public sealed record GetAvailableUsersQuery : IQuery<List<UserResponse>>;
