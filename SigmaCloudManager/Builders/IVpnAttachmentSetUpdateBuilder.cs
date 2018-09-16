using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IVpnAttachmentSetUpdateBuilder
    {
        IVpnAttachmentSetUpdateBuilder ForVpn(int? vpnId);
        IVpnAttachmentSetUpdateBuilder WithAttachmentSet(int? attachmentSetId);
        IVpnAttachmentSetUpdateBuilder WithHub(bool? isHub);
        IVpnAttachmentSetUpdateBuilder WithMulticastDirectlyIntegrated(bool? isMulticastDirectlyIntegrated);
        Task<VpnAttachmentSet> UpdateAsync();
    }
}
