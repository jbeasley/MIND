using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface ITenantCommunitySetService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<TenantCommunitySet>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<TenantCommunitySet>> GetAllByTenantIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<TenantCommunitySet>> GetAllByTenantCommunityIDAsync(int id, bool includeProperties = true);
        Task<TenantCommunitySet> GetByIDAsync(int id, bool includeProperties = true);
        Task<TenantCommunitySet> GetByNameAsync(string name, bool includeProperties = true);
        Task<int> AddAsync(TenantCommunitySet tenantCommunitySet);
        Task<int> UpdateAsync(TenantCommunitySet tenantCommunitySet);
        Task<int> DeleteAsync(TenantCommunitySet tenantCommunitySet);
    }
}
