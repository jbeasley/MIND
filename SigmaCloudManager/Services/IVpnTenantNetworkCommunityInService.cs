using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnTenantNetworkCommunityInService
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<VpnTenantNetworkCommunityIn>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkCommunityIn>> GetAllByVpnTenantNetworkInIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkCommunityIn>> GetAllByTenantCommunityIDAsync(int id, bool includeProperties = true);
        Task<VpnTenantNetworkCommunityIn> GetByIDAsync(int id, bool includeProperties = true);
        Task<int> AddAsync(VpnTenantNetworkCommunityIn vpnTenantNetworkCommunityIn);
        Task<int> UpdateAsync(VpnTenantNetworkCommunityIn vpnTenantNetworkCommunityIn);
        Task<int> DeleteAsync(VpnTenantNetworkCommunityIn vpnTenantNetworkCommunityIn);
    }
}
