using SCM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for updating attachments. The builder exposes a fluent API.
    /// </summary>
    public class AttachmentSetUpdateBuilder : AttachmentSetBuilder, IAttachmentSetUpdateBuilder
    {
        public AttachmentSetUpdateBuilder(IUnitOfWork unitOfWork, IAttachmentSetRoutingInstanceBuilder attachmentSetRoutingInstanceBuilder) : 
            base(unitOfWork, attachmentSetRoutingInstanceBuilder)
        {
        }

        public IAttachmentSetUpdateBuilder ForAttachmentSet(AttachmentSet attachmentSet)
        {
            _attachmentSet = attachmentSet;
            return this;
        }

        IAttachmentSetUpdateBuilder IAttachmentSetUpdateBuilder.WithAttachmentRedundancy(string attachmentRedundancy)
        {
            base.WithAttachmentRedundancy(attachmentRedundancy);
            return this;
        }

        IAttachmentSetUpdateBuilder IAttachmentSetUpdateBuilder.WithMulticastVpnDomainType(string multicastVpnDomainTypeName)
        {
            base.WithMulticastVpnDomainType(multicastVpnDomainTypeName);
            return this;
        }

        IAttachmentSetUpdateBuilder IAttachmentSetUpdateBuilder.WithSubRegion(string subregion)
        {
            base.WithSubRegion(subregion);
            return this;
        }

        public async Task<AttachmentSet> UpdateAsync()
        {
            if (_args.ContainsKey(nameof(WithMulticastVpnDomainType))) await base.SetMulticastVpnDomainTypeAsync();
            if (_args.ContainsKey(nameof(WithSubRegion))) await base.SetSubRegionAsync();
            if (_args.ContainsKey(nameof(WithAttachmentRedundancy))) await base.SetAttachmentRedundancyAsync();

            return _attachmentSet;
        }
    }
}
