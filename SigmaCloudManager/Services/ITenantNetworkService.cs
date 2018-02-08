using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface ITenantNetworkService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<TenantNetwork>> GetAllAsync(string searchString = "", bool includeProperties = true);
        Task<IEnumerable<TenantNetwork>> GetAllByTenantIDAsync(int id, string searchString = "", bool includeProperties = true);
        Task<TenantNetwork> GetByIDAsync(int id, bool includeProperties = true);
        Task<TenantNetwork> GetByCidrNameAsync(string cidrName, bool includeProperties = true);
        Task<int> AddAsync(TenantNetwork tenantNetwork);
        Task<int> UpdateAsync(TenantNetwork tenantNetwork);
        Task<int> DeleteAsync(TenantNetwork tenantNetwork);
    }
}
