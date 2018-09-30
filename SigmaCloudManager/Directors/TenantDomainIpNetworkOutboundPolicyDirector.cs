using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainIpNetworkOutboundPolicyDirector : ITenantDomainIpNetworkOutboundPolicyDirector
    {
        private readonly ITenantIpNetworkOutboundPolicyBuilder _builder;

        public TenantDomainIpNetworkOutboundPolicyDirector(ITenantIpNetworkOutboundPolicyBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantIpNetworkOut> BuildAsync(int deviceId, TenantDomainIpNetworkOutboundPolicyRequest request)
        {
            return await _builder.ForDevice(deviceId)
                                 .WithAdvertisedIpRoutingPreference(request.AdvertisedIpRoutingPreference)
                                 .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                 .WithTenantIpNetworkCidrName(request.TenantIpNetworkCidrName)
                                 .BuildAsync();
        }
    }
}
