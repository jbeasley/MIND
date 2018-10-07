using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface ITenantCommunityOutboundPolicyBuilder
    {
        ITenantCommunityOutboundPolicyBuilder ForAttachmentSet(int? attachmenSetId);
        ITenantCommunityOutboundPolicyBuilder ForDevice(int? deviceId);
        ITenantCommunityOutboundPolicyBuilder ForTenantCommunityOutboundPolicy(int vpnTenantCommunityOutId);
        ITenantCommunityOutboundPolicyBuilder WithTenantCommunityName(string tenantCommunityName);
        ITenantCommunityOutboundPolicyBuilder WithAdvertisedIpRoutingPreference(int? localIpRoutingPreference);
        ITenantCommunityOutboundPolicyBuilder WithIpv4PeerAddress(string peerIpv4Address);
        Task<VpnTenantCommunityOut> BuildAsync();
    }
}
