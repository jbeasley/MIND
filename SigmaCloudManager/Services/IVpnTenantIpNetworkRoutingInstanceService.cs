using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnTenantIpNetworkRoutingInstanceService : IBaseService
    {
        Task<IEnumerable<VpnTenantIpNetworkRoutingInstance>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<VpnTenantIpNetworkRoutingInstance>> GetAllByVpnIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkRoutingInstance> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkRoutingInstance> AddAsync(VpnTenantIpNetworkRoutingInstance vpnTenantNetworkRoutingInstance);
        Task<VpnTenantIpNetworkRoutingInstance> UpdateAsync(VpnTenantIpNetworkRoutingInstance vpnTenantNetworkRoutingInstance);
        Task DeleteAsync(int vpnTenantNetworkRoutingInstanceId);
    }
}
