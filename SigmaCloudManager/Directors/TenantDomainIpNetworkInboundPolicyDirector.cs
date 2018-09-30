using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainIpNetworkInboundPolicyDirector : ITenantDomainIpNetworkInboundPolicyDirector
    {
        private readonly ITenantIpNetworkInboundPolicyBuilder _builder;

        public TenantDomainIpNetworkInboundPolicyDirector(ITenantIpNetworkInboundPolicyBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantIpNetworkIn> BuildAsync(int deviceId, TenantDomainIpNetworkInboundPolicyRequest request)
        {
            return await _builder.ForDevice(deviceId)
                                 .WithTenantOwner(request.TenantId)
                                 .WithLocalIpRoutingPreference(request.LocalIpRoutingPreference)
                                 .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                 .WithTenantIpNetworkCidrName(request.TenantIpNetworkCidrName)
                                 .BuildAsync();
        }
    }
}
