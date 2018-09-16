using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IVpnAttachmentSetBuilder
    {
        IVpnAttachmentSetBuilder ForVpn(int? vpnId);
        IVpnAttachmentSetBuilder WithAttachmentSet(string attachmentSetName);
        IVpnAttachmentSetBuilder WithHub(bool? isHub);
        IVpnAttachmentSetBuilder WithMulticastDirectlyIntegrated(bool? isMulticastDirectlyIntegrated);
        Task<VpnAttachmentSet> BuildAsync();
    }
}
