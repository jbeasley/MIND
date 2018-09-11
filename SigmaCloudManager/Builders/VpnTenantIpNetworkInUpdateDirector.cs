using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class VpnTenantIpNetworkInUpdateDirector : IVpnTenantIpNetworkInUpdateDirector
    {
        private readonly IVpnTenantIpNetworkInUpdateBuilder _builder;

        public VpnTenantIpNetworkInUpdateDirector(IVpnTenantIpNetworkInUpdateBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantIpNetworkIn> UpdateAsync(int vpnTenantIpNetworkInId, VpnTenantIpNetworkInRequest request)
        {
            return await _builder.ForVpnTenantIpNetworkIn(vpnTenantIpNetworkInId)
                                 .WithLocalIpRoutingPreference(request.LocalIpRoutingPreference)
                                 .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                 .AddToAllBgpPeersInAttachmentSet(request.AddToAllBgpPeersInAttachmentSet)
                                 .UpdateAsync();
        }
    }
}
