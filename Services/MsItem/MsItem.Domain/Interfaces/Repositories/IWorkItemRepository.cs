using MsItem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsItem.Domain.Interfaces.Repositories;
public interface IWorkItemRepository : IBaseRepository<WorkItem>
{
    Task<List<WorkItem>> GetByUsernameAsync(string username, CancellationToken ct = default);
    Task<List<WorkItem>> GetPendingAsync(CancellationToken ct = default);
}
