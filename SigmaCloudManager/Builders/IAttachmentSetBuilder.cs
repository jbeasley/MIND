using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IAttachmentSetBuilder
    {
        IAttachmentSetBuilder ForTenant(int tenantId);
        IAttachmentSetBuilder WithAttachmentRedundancy(string attachmentRedundancy);
        IAttachmentSetBuilder WithLayer3(bool? isLayer3);
        IAttachmentSetBuilder WithMulticastVpnDomainType(string multicastVpnDomainTypeName);
        IAttachmentSetBuilder WithRegion(string region);
        IAttachmentSetBuilder WithSubRegion(string subregion);
        IAttachmentSetBuilder WithRoutingInstances(List<RoutingInstanceForAttachmentSetRequest> requests);
        Task<AttachmentSet> BuildAsync();
    }
}
