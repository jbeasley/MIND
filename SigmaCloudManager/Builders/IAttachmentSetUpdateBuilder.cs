using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;

namespace Mind.Builders
{
    public interface IAttachmentSetUpdateBuilder
    {
        IAttachmentSetUpdateBuilder ForAttachmentSet(AttachmentSet attachmentSet);
        IAttachmentSetUpdateBuilder WithAttachmentRedundancy(string attachmentRedundancy);
        IAttachmentSetUpdateBuilder WithMulticastVpnDomainType(string multicastVpnDomainTypeName);
        IAttachmentSetUpdateBuilder WithSubRegion(string subregion);
        Task<AttachmentSet> UpdateAsync();
    }
}
