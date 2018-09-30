using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainIpNetworkInboundPolicyUpdateDirector : ITenantDomainIpNetworkInboundPolicyUpdateDirector
    {
        private readonly ITenantIpNetworkInboundPolicyBuilder _builder;

        public TenantDomainIpNetworkInboundPolicyUpdateDirector(ITenantIpNetworkInboundPolicyBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantIpNetworkIn> UpdateAsync(int vpnTenantIpNetworkInId, TenantDomainIpNetworkInboundPolicyUpdate update)
        {
            return await _builder.ForTenantIpNetworkInboundPolicy(vpnTenantIpNetworkInId)
                                 .WithLocalIpRoutingPreference(update.LocalIpRoutingPreference)
                                 .WithIpv4PeerAddress(update.Ipv4PeerAddress)
                                 .BuildAsync();
        }
    }
}
