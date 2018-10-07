using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface ITenantCommunityBuilder
    {
        ITenantCommunityBuilder ForTenant(int? tenantId);
        ITenantCommunityBuilder ForTenantCommunity(int? tenantCommunityId);
        ITenantCommunityBuilder WithAsNumber(int? asNumber);
        ITenantCommunityBuilder WithNumber(int? number);
        ITenantCommunityBuilder WithAllowExtranet(bool? allowExtranet);
        ITenantCommunityBuilder WithIpRoutingBehaviour(string ipRoutingBehaviour);
        Task<TenantCommunity> BuildAsync();
    }
}
