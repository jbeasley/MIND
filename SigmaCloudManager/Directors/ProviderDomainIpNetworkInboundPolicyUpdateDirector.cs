using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainIpNetworkInboundPolicyUpdateDirector : IProviderDomainIpNetworkInboundPolicyUpdateDirector
    {
        private readonly ITenantIpNetworkInboundPolicyBuilder _builder;

        public ProviderDomainIpNetworkInboundPolicyUpdateDirector(ITenantIpNetworkInboundPolicyBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantIpNetworkIn> UpdateAsync(int vpnTenantIpNetworkInId, VpnTenantIpNetworkInUpdate update)
        {
            return await _builder.ForTenantIpNetworkInboundPolicy(vpnTenantIpNetworkInId)
                                 .WithLocalIpRoutingPreference(update.LocalIpRoutingPreference)
                                 .WithIpv4PeerAddress(update.Ipv4PeerAddress)
                                 .AddToAllBgpPeersInAttachmentSet(update.AddToAllBgpPeersInAttachmentSet)
                                 .BuildAsync();
        }
    }
}
