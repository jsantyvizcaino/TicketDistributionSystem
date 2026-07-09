using Mediator;
using MsUser.Application.Features.Users.DTOs;
using MsUser.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Application.Features.Users.Queries.GetAvailableUsers;
public sealed class GetAvailableUsersQueryHandler : IQueryHandler<GetAvailableUsersQuery, List<UserResponse>>
{
    private readonly IUserRepository _repository;

    public GetAvailableUsersQueryHandler(IUserRepository repository) => _repository = repository;

    public async ValueTask<List<UserResponse>> Handle(GetAvailableUsersQuery query, CancellationToken ct)
    {
        var users = await _repository.GetAvailableForDistributionAsync(ct);

        return users.Select(u => new UserResponse(
            u.Id,
            u.Username,
            u.Email,
            u.FullName,
            u.IsActive,
            u.TotalAssignedItems,
            u.CompletedItems,
            u.PendingItems,
            u.HighRelevanceItems,
            u.IsSaturated()
        )).ToList();
    }
}