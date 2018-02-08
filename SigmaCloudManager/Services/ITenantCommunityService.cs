using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface ITenantCommunityService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<TenantCommunity>> GetAllAsync(string searchString = "", bool includeProperties = true);
        Task<IEnumerable<TenantCommunity>> GetAllByTenantCommunitySetIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<TenantCommunity>> GetAllByTenantIDAsync(int id, string searchString = "", bool includeProperties = true);
        Task<TenantCommunity> GetByIDAsync(int id, bool includeProperties = true);
        Task<TenantCommunity> GetByCommunityAsync(int asNumber, int number, bool includeProperties = true);
        Task<int> AddAsync(TenantCommunity tenantCommunity);
        Task<int> UpdateAsync(TenantCommunity tenantCommunity);
        Task<int> DeleteAsync(TenantCommunity tenantCommunity);
    }
}
