﻿using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IProviderDomainIpNetworkOutboundPolicyDirector
    {
        Task<VpnTenantIpNetworkOut> BuildAsync(int attachmentSetId, VpnTenantIpNetworkOutRequest request);
        Task<VpnTenantIpNetworkOut> BuildAsync(AttachmentSet attachmentSet, VpnTenantIpNetworkOutRequest request);
        Task<List<VpnTenantIpNetworkOut>> BuildAsync(AttachmentSet attachmentSet, List<VpnTenantIpNetworkOutRequest> requests);
    }
}
