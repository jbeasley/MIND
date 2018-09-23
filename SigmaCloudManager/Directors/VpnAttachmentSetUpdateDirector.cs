using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class VpnAttachmentSetUpdateDirector : IVpnAttachmentSetUpdateDirector
    {
        private readonly IVpnAttachmentSetUpdateBuilder _builder;

        public VpnAttachmentSetUpdateDirector(IVpnAttachmentSetUpdateBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnAttachmentSet> UpdateAsync(int vpnId, int attachmentSetId, VpnAttachmentSetUpdate update)
        {
            return await _builder.ForVpn(vpnId)
                                 .WithAttachmentSet(attachmentSetId)
                                 .WithHub(update.IsHub)
                                 .WithMulticastDirectlyIntegrated(update.IsMulticastDirectlyIntegrated)
                                 .UpdateAsync();
        }
    }
}
