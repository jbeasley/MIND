using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnTenantIpNetworkStaticRouteRoutingInstanceService : IBaseService
    {
        Task<IEnumerable<VpnTenantIpNetworkStaticRouteRoutingInstance>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<VpnTenantIpNetworkStaticRouteRoutingInstance>> GetAllByVpnIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkStaticRouteRoutingInstance> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkStaticRouteRoutingInstance> AddAsync(VpnTenantIpNetworkStaticRouteRoutingInstance vpnTenantNetworkStaticRouteRoutingInstance);
        Task<VpnTenantIpNetworkStaticRouteRoutingInstance> UpdateAsync(VpnTenantIpNetworkStaticRouteRoutingInstance vpnTenantNetworkStaticRouteRoutingInstance);
        Task DeleteAsync(int vpnTenantNetworkStaticRouteRoutingInstanceId);
    }
}
