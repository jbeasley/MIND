using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IVpnTenantIpNetworkInBuilder
    {
        IVpnTenantIpNetworkInBuilder ForAttachmentSet(int? attachmenSetId);
        IVpnTenantIpNetworkInBuilder WithTenantOwner(int? tenantId);
        IVpnTenantIpNetworkInBuilder WithTenantIpNetworkCidrName(string tenantIpNetworkCidrName);
        IVpnTenantIpNetworkInBuilder WithLocalIpRoutingPreference(int? localIpRoutingPreference);
        IVpnTenantIpNetworkInBuilder AddToAllBgpPeersInAttachmentSet(bool? addToAllBgpPeersInAttachmentSet);
        IVpnTenantIpNetworkInBuilder WithIpv4PeerAddress(string peerIpv4Address);
        Task<VpnTenantIpNetworkIn> BuildAsync();
    }
}
