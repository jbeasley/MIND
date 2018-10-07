using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface ITenantCommunityInboundPolicyBuilder
    {
        ITenantCommunityInboundPolicyBuilder ForAttachmentSet(int? attachmenSetId);
        ITenantCommunityInboundPolicyBuilder ForDevice(int? deviceId);
        ITenantCommunityInboundPolicyBuilder ForTenantCommunityInboundPolicy(int vpnTenantCommunityInId);
        ITenantCommunityInboundPolicyBuilder WithTenantOwner(int? tenantId);
        ITenantCommunityInboundPolicyBuilder WithTenantCommunityName(string tenantCommunityName);
        ITenantCommunityInboundPolicyBuilder WithLocalIpRoutingPreference(int? localIpRoutingPreference);
        ITenantCommunityInboundPolicyBuilder AddToAllBgpPeersInAttachmentSet(bool? addToAllBgpPeersInAttachmentSet);
        ITenantCommunityInboundPolicyBuilder WithIpv4PeerAddress(string peerIpv4Address);
        Task<VpnTenantCommunityIn> BuildAsync();
    }
}
