using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface ITenantCommunitySetCommunityService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<TenantCommunitySetCommunity>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<TenantCommunitySetCommunity>> GetAllByTenantCommunitySetIDAsync(int id, bool includeProperties = true);
        Task<TenantCommunitySetCommunity> GetByIDAsync(int id, bool includeProperties = true);
        Task<int> AddAsync(TenantCommunitySetCommunity tenantCommunitySetCommunity);
        Task<int> DeleteAsync(TenantCommunitySetCommunity tenantCommunitySetCommunity);
    }
}
