using Mediator;
using MsUser.Application.Features.Users.DTOs;
using MsUser.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Application.Features.Users.Commands.UpdateUserWorkload;
public sealed class UpdateUserWorkloadCommandHandler : ICommandHandler<UpdateUserWorkloadCommand, UserResponse>
{
    private readonly IUserRepository _repository;

    public UpdateUserWorkloadCommandHandler(IUserRepository repository) => _repository = repository;

    public async ValueTask<UserResponse> Handle(UpdateUserWorkloadCommand command, CancellationToken ct)
    {
        var user = await _repository.GetByUsernameAsync(command.Username, ct)
            ?? throw new KeyNotFoundException($"Usuario {command.Username} no encontrado");

        if (command.IncrementAssigned > 0)
            user.TotalAssignedItems += command.IncrementAssigned;

        if (command.IncrementPending > 0)
            user.PendingItems += command.IncrementPending;

        if (command.IncrementHighRelevance > 0)
            user.HighRelevanceItems += command.IncrementHighRelevance;

        if (command.IsCompletion && user.PendingItems > 0)
        {
            user.CompletedItems++;
            user.PendingItems--;
        }

        user.UpdatedAt = DateTime.UtcNow;
        _repository.Update(user);
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