using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface ITenantIpNetworkInboundPolicyBuilder
    {
        ITenantIpNetworkInboundPolicyBuilder ForAttachmentSet(int? attachmenSetId);
        ITenantIpNetworkInboundPolicyBuilder ForDevice(int? deviceId);
        ITenantIpNetworkInboundPolicyBuilder ForTenantIpNetworkInboundPolicy(int vpnTenantIpNetworkInId);
        ITenantIpNetworkInboundPolicyBuilder WithTenantOwner(int? tenantId);
        ITenantIpNetworkInboundPolicyBuilder WithTenantIpNetworkCidrName(string tenantIpNetworkCidrName);
        ITenantIpNetworkInboundPolicyBuilder WithLocalIpRoutingPreference(int? localIpRoutingPreference);
        ITenantIpNetworkInboundPolicyBuilder AddToAllBgpPeersInAttachmentSet(bool? addToAllBgpPeersInAttachmentSet);
        ITenantIpNetworkInboundPolicyBuilder WithIpv4PeerAddress(string peerIpv4Address);
        Task<VpnTenantIpNetworkIn> BuildAsync();
    }
}
