using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MsItem.Infrastructure.Http.Providers.Refit;

public sealed class RefitUserApiClient : IUserApiClient
{
    private readonly IRefitUserApi _api;

    public RefitUserApiClient(IRefitUserApi api) => _api = api;

    public async Task<List<AvailableUserDto>> GetAvailableUsersAsync(CancellationToken ct = default) =>
        await _api.GetAvailableUsersAsync();

    public async Task UpdateUserWorkloadAsync(string username, UpdateUserWorkloadDto dto, CancellationToken ct = default) =>
        await _api.UpdateUserWorkloadAsync(username, dto);
}
