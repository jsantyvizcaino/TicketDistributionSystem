using Mediator;
using MsUser.Application.Features.Users.DTOs;
using MsUser.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Application.Features.Users.Queries.GetUserByUsername;

/// <summary>
/// Maneja la búsqueda de un usuario por su nombre de usuario.
/// </summary>
public sealed class GetUserByUsernameQueryHandler : IQueryHandler<GetUserByUsernameQuery, UserResponse>
{
    private readonly IUserRepository _repository;

    public GetUserByUsernameQueryHandler(IUserRepository repository) => _repository = repository;

    /// <summary>
    /// Ejecuta la consulta: busca al usuario por nombre de usuario o lanza
    /// <see cref="KeyNotFoundException"/> si no existe.
    /// </summary>
    public async ValueTask<UserResponse> Handle(GetUserByUsernameQuery query, CancellationToken ct)
    {
        var user = await _repository.GetByUsernameAsync(query.Username, ct)
            ?? throw new KeyNotFoundException($"Usuario {query.Username} no encontrado");

        return new UserResponse(
            user.Id,
            user.Username,
            user.Email,
            user.FullName,
            user.IsActive,
            user.TotalAssignedItems,
            user.CompletedItems,
            user.PendingItems,
            user.HighRelevanceItems,
            user.IsSaturated()
        );
    }
}