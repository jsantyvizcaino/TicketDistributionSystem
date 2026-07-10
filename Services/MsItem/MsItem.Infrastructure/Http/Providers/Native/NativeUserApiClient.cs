using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Application.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MsItem.Infrastructure.Http.Providers.Native;

public sealed class NativeUserApiClient : IUserApiClient
{
    private readonly HttpClient _httpClient;

    public NativeUserApiClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<List<AvailableUserDto>> GetAvailableUsersAsync(CancellationToken ct = default)
    {
        var result = await _httpClient.GetFromJsonAsync<List<AvailableUserDto>>("/api/users/available", ct);
        return result ?? [];
    }

    public async Task UpdateUserWorkloadAsync(string username, UpdateUserWorkloadDto dto, CancellationToken ct = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/users/{username}/workload", dto, ct);
        response.EnsureSuccessStatusCode();
    }
}
