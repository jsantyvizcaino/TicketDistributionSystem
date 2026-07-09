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
public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserResponse>
{
    private readonly IUserRepository _repository;

    public CreateUserCommandHandler(IUserRepository repository) => _repository = repository;

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