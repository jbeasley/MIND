using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class VpnAttachmentSetDirector : IVpnAttachmentSetDirector
    {
        private readonly IVpnAttachmentSetBuilder _builder;

        public VpnAttachmentSetDirector(IVpnAttachmentSetBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnAttachmentSet> BuildAsync(int vpnId, VpnAttachmentSetRequest request)
        {
            return await _builder.ForVpn(vpnId)
                                 .WithAttachmentSet(request.AttachmentSetName)
                                 .WithHub(request.IsHub)
                                 .WithMulticastDirectlyIntegrated(request.IsMulticastDirectlyIntegrated)
                                 .BuildAsync();
        }
    }
}
