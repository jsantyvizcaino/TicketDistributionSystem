using MsItem.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MsItem.Application.Features.WorkItems.Services;

public interface IDistributionService
{
    Task<string> DistributeAsync(WorkItem item, CancellationToken ct = default);
}
