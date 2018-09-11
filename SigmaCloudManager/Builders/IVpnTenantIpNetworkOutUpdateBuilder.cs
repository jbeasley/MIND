using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IVpnTenantIpNetworkOutUpdateBuilder
    {
        IVpnTenantIpNetworkOutUpdateBuilder ForVpnTenantIpNetworkOut(int VpnTenantIpNetworkOutId);
        IVpnTenantIpNetworkOutUpdateBuilder WithAdvertisedIpRoutingPreference(int? localIpRoutingPreference);
        IVpnTenantIpNetworkOutUpdateBuilder WithIpv4PeerAddress(string peerIpv4Address);
        Task<VpnTenantIpNetworkOut> UpdateAsync();
    }
}
