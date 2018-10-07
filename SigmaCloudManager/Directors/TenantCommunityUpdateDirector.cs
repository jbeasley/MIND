using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantCommunityUpdateDirector : ITenantCommunityUpdateDirector
    {
        private readonly ITenantCommunityBuilder _builder;
        public TenantCommunityUpdateDirector(ITenantCommunityBuilder builder)
        {
            _builder = builder;
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
