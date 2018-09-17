using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnTenantIpNetworkRoutingInstanceStaticRouteService : IBaseService
    {
        Task<IEnumerable<VpnTenantIpNetworkRoutingInstanceStaticRoute>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<VpnTenantIpNetworkRoutingInstanceStaticRoute>> GetAllByVpnIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> AddAsync(VpnTenantIpNetworkRoutingInstanceStaticRoute vpnTenantNetworkStaticRouteRoutingInstance);
        Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> UpdateAsync(VpnTenantIpNetworkRoutingInstanceStaticRoute vpnTenantNetworkStaticRouteRoutingInstance);
        Task DeleteAsync(int vpnTenantNetworkStaticRouteRoutingInstanceId);
    }
}
