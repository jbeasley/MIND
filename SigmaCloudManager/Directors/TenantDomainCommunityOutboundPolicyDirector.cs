using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainCommunityOutboundPolicyDirector : ITenantDomainCommunityOutboundPolicyDirector
    {
        private readonly ITenantCommunityOutboundPolicyBuilder _builder;

        public TenantDomainCommunityOutboundPolicyDirector(ITenantCommunityOutboundPolicyBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantCommunityOut> BuildAsync(int deviceId, TenantDomainCommunityOutboundPolicyRequest request)
        {
            return await _builder.ForDevice(deviceId)
                                 .WithAdvertisedIpRoutingPreference(request.AdvertisedIpRoutingPreference)
                                 .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                 .WithTenantCommunityName(request.TenantCommunityName)
                                 .BuildAsync();
        }
    }
}
