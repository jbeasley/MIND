using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface ITenantIpNetworkUpdateBuilder
    {
        ITenantIpNetworkUpdateBuilder ForTenantIpNetwork(int? tenantIpNetworkId);
        ITenantIpNetworkUpdateBuilder WithIpv4Prefix(string ipv4Prefix);
        ITenantIpNetworkUpdateBuilder WithIpv4Length(int? ipv4Length);
        ITenantIpNetworkUpdateBuilder WithIpv4LessThanOrEqualToLength(int? ipv4LessThanOrEqualToLength);
        ITenantIpNetworkUpdateBuilder WithAllowExtranet(bool? allowExtranet);
        ITenantIpNetworkUpdateBuilder WithIpRoutingBehaviour(string ipRoutingBehaviour);
        Task<TenantIpNetwork> UpdateAsync();
    }
}
