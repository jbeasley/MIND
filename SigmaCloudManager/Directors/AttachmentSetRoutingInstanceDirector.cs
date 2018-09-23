using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class AttachmentSetRoutingInstanceDirector : IAttachmentSetRoutingInstanceDirector
    {
        private readonly IAttachmentSetRoutingInstanceBuilder _builder;

        public AttachmentSetRoutingInstanceDirector(IAttachmentSetRoutingInstanceBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.AttachmentSetRoutingInstance> BuildAsync(int attachmentSetId, RoutingInstanceForAttachmentSetRequest request)
        {
            return await _builder.ForAttachmentSet(attachmentSetId)
                                 .WithRoutingInstance(request.RoutingInstanceName)
                                 .WithAdvertisedIpRoutingPreference(request.AdvertisedIpRoutingPreference)
                                 .WithLocalIpRoutingPreference(request.LocalIpRoutingPreference)
                                 .WithMulticastDesignatedRouterPreference(request.MulticastDesignatedRouterPreference)
                                 .BuildAsync();
        }
    }
}
