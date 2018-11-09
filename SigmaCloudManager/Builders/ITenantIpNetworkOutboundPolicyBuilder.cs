using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface ITenantIpNetworkOutboundPolicyBuilder
    {
        ITenantIpNetworkOutboundPolicyBuilder ForAttachmentSet(int? attachmenSetId);
        ITenantIpNetworkOutboundPolicyBuilder ForAttachmentSet(AttachmentSet attachmenSet);
        ITenantIpNetworkOutboundPolicyBuilder ForDevice(int? deviceId);
        ITenantIpNetworkOutboundPolicyBuilder ForTenantIpNetworkOutboundPolicy(int vpnTenantIpNetworkOutId);
        ITenantIpNetworkOutboundPolicyBuilder WithTenantIpNetworkCidrName(string tenantIpNetworkCidrName);
        ITenantIpNetworkOutboundPolicyBuilder WithAdvertisedIpRoutingPreference(int? localIpRoutingPreference);
        ITenantIpNetworkOutboundPolicyBuilder WithIpv4PeerAddress(string peerIpv4Address);
        Task<VpnTenantIpNetworkOut> BuildAsync();
    }
}
