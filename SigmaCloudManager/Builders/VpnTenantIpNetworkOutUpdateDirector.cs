using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class VpnTenantIpNetworkOutUpdateDirector : IVpnTenantIpNetworkOutUpdateDirector
    {
        private readonly IVpnTenantIpNetworkOutUpdateBuilder _builder;

        public VpnTenantIpNetworkOutUpdateDirector(IVpnTenantIpNetworkOutUpdateBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantIpNetworkOut> UpdateAsync(int vpnTenantIpNetworkOutId, VpnTenantIpNetworkOutRequest request)
        {
            return await _builder.ForVpnTenantIpNetworkOut(vpnTenantIpNetworkOutId)
                                 .WithAdvertisedIpRoutingPreference(request.AdvertisedIpRoutingPreference)
                                 .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                 .UpdateAsync();
        }
    }
}
