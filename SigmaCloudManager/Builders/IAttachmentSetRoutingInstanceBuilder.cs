using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IAttachmentSetRoutingInstanceBuilder
    {
        IAttachmentSetRoutingInstanceBuilder ForAttachmentSet(int? attachmenSetId);
        IAttachmentSetRoutingInstanceBuilder ForAttachmentSet(AttachmentSet attachmenSet);
        IAttachmentSetRoutingInstanceBuilder WithRoutingInstance(string routingInstanceName);
        IAttachmentSetRoutingInstanceBuilder WithLocalIpRoutingPreference(int? localIpRoutingPreference);
        IAttachmentSetRoutingInstanceBuilder WithAdvertisedIpRoutingPreference(int? advertisedIpRoutingPreference);
        IAttachmentSetRoutingInstanceBuilder WithMulticastDesignatedRouterPreference(int? multicastDesignatedRouterPreference);
        Task<AttachmentSetRoutingInstance> BuildAsync();
    }
}
