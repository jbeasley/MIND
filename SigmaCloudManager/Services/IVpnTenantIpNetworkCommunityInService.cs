using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnTenantIpNetworkCommunityInService :IBaseService
    {
        Task<IEnumerable<VpnTenantIpNetworkCommunityIn>> GetAllByVpnTenantIpNetworkInIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkCommunityIn> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkCommunityIn> AddAsync(VpnTenantIpNetworkCommunityIn vpnTenantIpNetworkCommunityIn);
        Task<VpnTenantIpNetworkCommunityIn> UpdateAsync(VpnTenantIpNetworkCommunityIn vpnTenantIpNetworkCommunityIn);
        Task DeleteAsync(int vpnTenantNetworkCommunityInID);
    }
}
