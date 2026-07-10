using MsUser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsUser.Domain.Interfaces.Repositories;
public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default);
    Task<List<User>> GetAvailableForDistributionAsync(CancellationToken ct = default);
}