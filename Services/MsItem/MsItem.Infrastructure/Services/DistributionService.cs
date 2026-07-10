using MsItem.Application.Features.WorkItems.DTOs;
using MsItem.Application.Features.WorkItems.Services;
using MsItem.Application.Features.WorkItems.Strategies;
using MsItem.Application.Interfaces;
using MsItem.Domain.Entities;
using MsItem.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace MsItem.Infrastructure.Services;

public sealed class DistributionService : IDistributionService
{
    private readonly IUserApiClient _userApiClient;
    private readonly DistributionContext _distributionContext;

    public DistributionService(IUserApiClient userApiClient, DistributionContext distributionContext)
    {
        _userApiClient = userApiClient;
        _distributionContext = distributionContext;
    }

    public async Task<string> DistributeAsync(WorkItem item, CancellationToken ct = default)
    {
        var candidates = await _userApiClient.GetAvailableUsersAsync(ct);

        var (user, _) = _distributionContext.Distribute(item, candidates);

        var workload = new UpdateUserWorkloadDto(
            user.Username,
            IncrementAssigned: 1,
            IncrementPending: 1,
            IncrementHighRelevance: item.Relevance == RelevanceLevel.High ? 1 : 0,
            IsCompletion: false
        );

        await _userApiClient.UpdateUserWorkloadAsync(user.Username, workload, ct);

        return user.Username;
    }
}
