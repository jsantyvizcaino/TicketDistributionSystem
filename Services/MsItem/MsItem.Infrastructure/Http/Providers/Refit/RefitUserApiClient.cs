using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MsItem.Infrastructure.Http.Providers.Refit;

/// <summary>
/// Implementación de <see cref="IUserApiClient"/> que delega en el cliente Refit
/// (<see cref="IRefitUserApi"/>) generado dinámicamente para consumir MsUser.
/// </summary>
public sealed class RefitUserApiClient : IUserApiClient
{
    private readonly IRefitUserApi _api;

    public RefitUserApiClient(IRefitUserApi api) => _api = api;

    /// <inheritdoc />
    public async Task<List<AvailableUserDto>> GetAvailableUsersAsync(CancellationToken ct = default) =>
        await _api.GetAvailableUsersAsync();

    /// <inheritdoc />
    public async Task UpdateUserWorkloadAsync(string username, UpdateUserWorkloadDto dto, CancellationToken ct = default) =>
        await _api.UpdateUserWorkloadAsync(username, dto);
}
