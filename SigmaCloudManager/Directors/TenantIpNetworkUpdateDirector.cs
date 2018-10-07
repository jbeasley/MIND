using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantIpNetworkUpdateDirector : ITenantIpNetworkUpdateDirector
    {
        private readonly ITenantIpNetworkBuilder _builder;
        public TenantIpNetworkUpdateDirector(ITenantIpNetworkBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.TenantIpNetwork> UpdateAsync(int tenantIpNetworkId, TenantIpNetworkRequest request)
        {
            return await _builder.ForTenantIpNetwork(tenantIpNetworkId)
                                 .WithIpv4Prefix(request.Ipv4Prefix)
                                 .WithIpv4Length(request.Ipv4Length)
                                 .WithIpv4LessThanOrEqualToLength(request.Ipv4LessThanOrEqualToLength)
                                 .WithAllowExtranet(request.AllowExtranet)
                                 .WithIpRoutingBehaviour(request.IpRoutingBehaviour.ToString())
                                 .BuildAsync();
        }
    }
}
