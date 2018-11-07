﻿using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IVpnAttachmentSetDirector
    {
        Task<VpnAttachmentSet> BuildAsync(int vpnId, VpnAttachmentSetRequest request);
        Task<VpnAttachmentSet> BuildAsync(Vpn vpn, VpnAttachmentSetRequest request);
        Task<List<SCM.Models.VpnAttachmentSet>> BuildAsync(Vpn vpn, List<VpnAttachmentSetRequest> requests);
    }
}
