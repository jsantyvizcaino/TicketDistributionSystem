using MsItem.Application.Features.WorkItems.DTOs;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsItem.Infrastructure.Http.Providers.Refit;

public interface IRefitUserApi
{
    [Get("/api/users/available")]
    Task<List<AvailableUserDto>> GetAvailableUsersAsync();

    [Put("/api/users/{username}/workload")]
    Task UpdateUserWorkloadAsync(string username, [Body] UpdateUserWorkloadDto dto);
}
