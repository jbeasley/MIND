using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IVpnTenantIpNetworkInUpdateBuilder
    {
        IVpnTenantIpNetworkInUpdateBuilder ForVpnTenantIpNetworkIn(int vpnTenantIpNetworkInId);
        IVpnTenantIpNetworkInUpdateBuilder WithLocalIpRoutingPreference(int? localIpRoutingPreference);
        IVpnTenantIpNetworkInUpdateBuilder AddToAllBgpPeersInAttachmentSet(bool? addToAllBgpPeersInAttachmentSet);
        IVpnTenantIpNetworkInUpdateBuilder WithIpv4PeerAddress(string peerIpv4Address);
        Task<VpnTenantIpNetworkIn> UpdateAsync();
    }
}
