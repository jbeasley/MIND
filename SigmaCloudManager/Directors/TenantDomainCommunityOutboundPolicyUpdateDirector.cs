using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainCommunityOutboundPolicyUpdateDirector : ITenantDomainCommunityOutboundPolicyUpdateDirector
    {
        private readonly ITenantCommunityOutboundPolicyBuilder _builder;

        public TenantDomainCommunityOutboundPolicyUpdateDirector(ITenantCommunityOutboundPolicyBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantCommunityOut> UpdateAsync(int vpnTenantCommunityOutId, TenantDomainCommunityOutboundPolicyUpdate update)
        {
            return await _builder.ForTenantCommunityOutboundPolicy(vpnTenantCommunityOutId)
                                 .WithAdvertisedIpRoutingPreference(update.AdvertisedIpRoutingPreference)
                                 .WithIpv4PeerAddress(update.Ipv4PeerAddress)
                                 .BuildAsync();
        }
    }
}
