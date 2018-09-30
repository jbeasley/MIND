using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainIpNetworkOutboundPolicyUpdateDirector : ITenantDomainIpNetworkOutboundPolicyUpdateDirector
    {
        private readonly ITenantIpNetworkOutboundPolicyBuilder _builder;

        public TenantDomainIpNetworkOutboundPolicyUpdateDirector(ITenantIpNetworkOutboundPolicyBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantIpNetworkOut> UpdateAsync(int vpnTenantIpNetworkOutId, TenantDomainIpNetworkOutboundPolicyUpdate update)
        {
            return await _builder.ForTenantIpNetworkOutboundPolicy(vpnTenantIpNetworkOutId)
                                 .WithAdvertisedIpRoutingPreference(update.AdvertisedIpRoutingPreference)
                                 .WithIpv4PeerAddress(update.Ipv4PeerAddress)
                                 .BuildAsync();
        }
    }
}
