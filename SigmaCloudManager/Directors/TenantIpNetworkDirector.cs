using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantIpNetworkDirector : ITenantIpNetworkDirector
    {
        private readonly ITenantIpNetworkBuilder _builder;
        public TenantIpNetworkDirector(ITenantIpNetworkBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.TenantIpNetwork> BuildAsync(int tenantId, TenantIpNetworkRequest request)
        {
            return await _builder.ForTenant(tenantId)
                                 .WithIpv4Prefix(request.Ipv4Prefix)
                                 .WithIpv4Length(request.Ipv4Length)
                                 .WithIpv4LessThanOrEqualToLength(request.Ipv4LessThanOrEqualToLength)
                                 .WithAllowExtranet(request.AllowExtranet)
                                 .WithIpRoutingBehaviour(request.IpRoutingBehaviour.ToString())
                                 .WithTenantEnvironment(request.TenantEnvironment.ToString())
                                 .BuildAsync();
        }
    }
}
