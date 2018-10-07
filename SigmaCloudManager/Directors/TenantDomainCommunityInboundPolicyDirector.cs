using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainCommunityInboundPolicyDirector : ITenantDomainCommunityInboundPolicyDirector
    {
        private readonly ITenantCommunityInboundPolicyBuilder _builder;

        public TenantDomainCommunityInboundPolicyDirector(ITenantCommunityInboundPolicyBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantCommunityIn> BuildAsync(int deviceId, TenantDomainCommunityInboundPolicyRequest request)
        {
            return await _builder.ForDevice(deviceId)
                                 .WithTenantOwner(request.TenantId)
                                 .WithLocalIpRoutingPreference(request.LocalIpRoutingPreference)
                                 .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                 .WithTenantCommunityName(request.TenantCommunityName)
                                 .BuildAsync();
        }
    }
}
