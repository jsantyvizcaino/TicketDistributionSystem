using Mediator;
using MsUser.Application.Features.Users.DTOs;
using MsUser.Domain.Entities;
using MsUser.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Application.Features.Users.Commands.CreateUser;

/// <summary>
/// Maneja la creación de un nuevo usuario, inicializando sus contadores de carga de trabajo en cero.
/// </summary>
public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserResponse>
{
    private readonly IUserRepository _repository;

    public CreateUserCommandHandler(IUserRepository repository) => _repository = repository;

    /// <summary>
    /// Ejecuta el comando: crea el usuario, lo persiste y devuelve su representación.
    /// </summary>
    public async ValueTask<UserResponse> Handle(CreateUserCommand command, CancellationToken ct)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = command.Username,
            Email = command.Email,
            FullName = command.FullName,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(user, ct);
        await _repository.SaveChangesAsync(ct);

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