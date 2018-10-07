using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class ProviderDomainCommunityInboundPolicyDirector : IProviderDomainCommunityInboundPolicyDirector
    {
        private readonly ITenantCommunityInboundPolicyBuilder _builder;

        public ProviderDomainCommunityInboundPolicyDirector(ITenantCommunityInboundPolicyBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantCommunityIn> BuildAsync(int attachmentSetId, VpnTenantCommunityInRequest request)
        {
            return await _builder.ForAttachmentSet(attachmentSetId)
                                 .WithTenantOwner(request.TenantId)
                                 .WithLocalIpRoutingPreference(request.LocalIpRoutingPreference)
                                 .WithIpv4PeerAddress(request.Ipv4PeerAddress)
                                 .WithTenantCommunityName(request.TenantCommunityName)
                                 .AddToAllBgpPeersInAttachmentSet(request.AddToAllBgpPeersInAttachmentSet)
                                 .BuildAsync();
        }
    }
}
