using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainCommunityOutboundPolicyDirector : IProviderDomainCommunityOutboundPolicyDirector
    {
        private readonly ITenantCommunityOutboundPolicyBuilder _builder;

        public ProviderDomainCommunityOutboundPolicyDirector(ITenantCommunityOutboundPolicyBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantCommunityOut> BuildAsync(int attachmentSetId, VpnTenantCommunityOutRequest request)
        {
            return await _builder.ForAttachmentSet(attachmentSetId)
                                 .WithAdvertisedIpRoutingPreference(request.AdvertisedIpRoutingPreference)
                                 .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                 .WithTenantCommunityName(request.TenantCommunityName)
                                 .BuildAsync();
        }
    }
}
