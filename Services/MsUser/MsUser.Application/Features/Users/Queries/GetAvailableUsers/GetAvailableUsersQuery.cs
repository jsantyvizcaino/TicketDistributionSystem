using Mediator;
using MsUser.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Application.Features.Users.Queries.GetAvailableUsers;
public sealed record GetAvailableUsersQuery : IQuery<List<UserResponse>>;
