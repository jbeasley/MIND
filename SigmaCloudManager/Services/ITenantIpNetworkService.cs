using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using SCM.Services;
using Mind.Models.RequestModels;

namespace Mind.Services
{
    public interface ITenantIpNetworkService : IBaseService
    { 
        Task<IEnumerable<TenantIpNetwork>> GetAllByTenantIDAsync(int id, string searchString = "", bool? deep = false, bool asTrackable = false);
        Task<TenantIpNetwork> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<TenantIpNetwork> AddAsync(TenantIpNetwork tenantNetwork);
        Task<TenantIpNetwork> AddAsync(int tenantId, TenantIpNetworkRequest request);
        Task<TenantIpNetwork> UpdateAsync(TenantIpNetwork tenantNetwork);
        Task<TenantIpNetwork> UpdateAsync(int tenantIpNetworkId, TenantIpNetworkRequest update);
        Task DeleteAsync(int tenantNetworkId);
    }
}
