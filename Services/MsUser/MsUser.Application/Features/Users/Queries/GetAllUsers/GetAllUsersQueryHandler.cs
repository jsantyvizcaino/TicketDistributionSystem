using Mediator;
using MsUser.Application.Features.Users.DTOs;
using MsUser.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Application.Features.Users.Queries.GetAllUsers;

/// <summary>
/// Maneja la obtención de todos los usuarios registrados.
/// </summary>
public sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, List<UserResponse>>
{
    private readonly IUserRepository _repository;

    public GetAllUsersQueryHandler(IUserRepository repository) => _repository = repository;

    /// <summary>
    /// Ejecuta la consulta y devuelve la representación de todos los usuarios.
    /// </summary>
    public async ValueTask<List<UserResponse>> Handle(GetAllUsersQuery query, CancellationToken ct)
    {
        var users = await _repository.GetAllAsync(ct);

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
