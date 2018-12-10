using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantCommunityDirector : ITenantCommunityDirector
    {
        private readonly ITenantCommunityBuilder _builder;
        public TenantCommunityDirector(ITenantCommunityBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.TenantCommunity> BuildAsync(int tenantId, TenantCommunityRequest request)
        {
            return await _builder.ForTenant(tenantId)
                                 .WithAsNumber(request.AutonomousSystemNumber)
                                 .WithNumber(request.Number)
                                 .WithAllowExtranet(request.AllowExtranet)
                                 .WithIpRoutingBehaviour(request.IpRoutingBehaviour.ToString())
                                 .BuildAsync();
        }

        public async Task<SCM.Models.TenantCommunity> UpdateAsync(int tenantCommunityId, TenantCommunityRequest request)
        {
            return await _builder.ForTenantCommunity(tenantCommunityId)
                                 .WithAsNumber(request.AutonomousSystemNumber)
                                 .WithNumber(request.Number)
                                 .WithAllowExtranet(request.AllowExtranet)
                                 .WithIpRoutingBehaviour(request.IpRoutingBehaviour.ToString())
                                 .BuildAsync();
        }
    }
}
