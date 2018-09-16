using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IVpnAttachmentSetUpdateDirector
    {
        Task<VpnAttachmentSet> UpdateAsync(int vpnId, int attachmentSetId, VpnAttachmentSetUpdate update);
    }
}
