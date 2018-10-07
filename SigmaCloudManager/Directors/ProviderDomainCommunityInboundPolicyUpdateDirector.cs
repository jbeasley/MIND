using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainCommunityInboundPolicyUpdateDirector : IProviderDomainCommunityInboundPolicyUpdateDirector
    {
        private readonly ITenantCommunityInboundPolicyBuilder _builder;

        public ProviderDomainCommunityInboundPolicyUpdateDirector(ITenantCommunityInboundPolicyBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantCommunityIn> UpdateAsync(int vpnTenantCommunityInId, VpnTenantCommunityInUpdate update)
        {
            return await _builder.ForTenantCommunityInboundPolicy(vpnTenantCommunityInId)
                                 .WithLocalIpRoutingPreference(update.LocalIpRoutingPreference)
                                 .WithIpv4PeerAddress(update.Ipv4PeerAddress)
                                 .AddToAllBgpPeersInAttachmentSet(update.AddToAllBgpPeersInAttachmentSet)
                                 .BuildAsync();
        }
    }
}
