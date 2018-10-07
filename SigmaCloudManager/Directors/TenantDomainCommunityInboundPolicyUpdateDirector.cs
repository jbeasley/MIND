using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainCommunityInboundPolicyUpdateDirector : ITenantDomainCommunityInboundPolicyUpdateDirector
    {
        private readonly ITenantCommunityInboundPolicyBuilder _builder;

        public TenantDomainCommunityInboundPolicyUpdateDirector(ITenantCommunityInboundPolicyBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantCommunityIn> UpdateAsync(int vpnTenantCommunityInId, TenantDomainCommunityInboundPolicyUpdate update)
        {
            return await _builder.ForTenantCommunityInboundPolicy(vpnTenantCommunityInId)
                                 .WithLocalIpRoutingPreference(update.LocalIpRoutingPreference)
                                 .WithIpv4PeerAddress(update.Ipv4PeerAddress)
                                 .BuildAsync();
        }
    }
}
