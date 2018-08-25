using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IVpnTenantIpNetworkOutBuilder
    {
        IVpnTenantIpNetworkOutBuilder ForAttachmentSet(int? attachmenSetId);
        IVpnTenantIpNetworkOutBuilder WithTenantIpNetworkCidrName(string tenantIpNetworkCidrName);
        IVpnTenantIpNetworkOutBuilder WithAdvertisedIpRoutingPreference(int? localIpRoutingPreference);
        IVpnTenantIpNetworkOutBuilder WithIpv4PeerAddress(string peerIpv4Address);
        Task<VpnTenantIpNetworkOut> BuildAsync();
    }
}
