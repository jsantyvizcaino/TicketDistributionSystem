using MsItem.Application.Features.WorkItems.DTOs;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsItem.Infrastructure.Http.Providers.Refit;

/// <summary>
/// Definición declarativa (Refit) de los endpoints HTTP de MsUser consumidos por MsItem.
/// </summary>
public interface IRefitUserApi
{
    /// <summary>
    /// Invoca <c>GET /api/users/available</c> en MsUser.
    /// </summary>
    [Get("/api/users/available")]
    Task<List<AvailableUserDto>> GetAvailableUsersAsync();

    /// <summary>
    /// Invoca <c>PUT /api/users/{username}/workload</c> en MsUser.
    /// </summary>
    [Put("/api/users/{username}/workload")]
    Task UpdateUserWorkloadAsync(string username, [Body] UpdateUserWorkloadDto dto);
}
