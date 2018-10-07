using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IProviderDomainCommunityOutboundPolicyDirector
    {
        Task<VpnTenantCommunityOut> BuildAsync(int attachmentSetId, VpnTenantCommunityOutRequest request);
    }
}
