using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IProviderDomainCommunityOutboundPolicyUpdateDirector
    {
        Task<VpnTenantCommunityOut> UpdateAsync(int vpnTenantCommunityOutId, VpnTenantCommunityOutUpdate update);
    }
}
