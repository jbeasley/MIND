using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IProviderDomainIpNetworkInboundPolicyDirector
    {
        Task<VpnTenantIpNetworkIn> BuildAsync(int attachmentSetId, VpnTenantIpNetworkInRequest request);
        Task<VpnTenantIpNetworkIn> BuildAsync(AttachmentSet attachmentSet, VpnTenantIpNetworkInRequest request);
        Task<List<VpnTenantIpNetworkIn>> BuildAsync(AttachmentSet attachmentSet, List<VpnTenantIpNetworkInRequest> requests);
    }
}
