using MsItem.Application.Features.WorkItems.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MsItem.Application.Interfaces;

public interface IUserApiClient
{
    Task<List<AvailableUserDto>> GetAvailableUsersAsync(CancellationToken ct = default);
    Task UpdateUserWorkloadAsync(string username, UpdateUserWorkloadDto dto, CancellationToken ct = default);
}
