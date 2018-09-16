﻿using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface ITenantIpNetworkBuilder
    {
        ITenantIpNetworkBuilder ForTenant(int? tenantId);
        ITenantIpNetworkBuilder WithIpv4Prefix(string ipv4Prefix);
        ITenantIpNetworkBuilder WithIpv4Length(int? ipv4Length);
        ITenantIpNetworkBuilder WithIpv4LessThanOrEqualToLength(int? ipv4LessThanOrEqualToLength);
        ITenantIpNetworkBuilder WithAllowExtranet(bool? allowExtranet);
        ITenantIpNetworkBuilder WithIpRoutingBehaviour(string ipRoutingBehaviour);
        Task<TenantIpNetwork> BuildAsync();
    }
}
