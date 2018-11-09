using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class AttachmentSetDirector : IAttachmentSetDirector
    {
        private readonly IAttachmentSetBuilder _builder;

        public AttachmentSetDirector(IAttachmentSetBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.AttachmentSet> BuildAsync(int tenantId, AttachmentSetRequest request)
        {
            return await _builder.ForTenant(tenantId)
                                 .WithAttachmentRedundancy(request.AttachmentRedundancy.ToString())
                                 .WithLayer3(request.IsLayer3)
                                 .WithRegion(request.Region.ToString())
                                 .WithSubRegion(request.SubRegion)
                                 .WithRoutingInstances(request.AttachmentSetRoutingInstances)
                                 .WithBgpIpNetworkInboundPolicy(request.BgpIpNetworkInboundPolicy)
                                 .WithBgpIpNetworkOutboundPolicy(request.BgpIpNetworkOutboundPolicy)
                                 .WithMulticastVpnDomainType(request.MulticastVpnDomainType.ToString())
                                 .BuildAsync();
        }
    }
}
