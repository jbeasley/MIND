﻿using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface IVrfRoutingInstanceBuilder : IRoutingInstanceBuilder
    {
        IVrfRoutingInstanceBuilder ForDevice(int deviceId);
        IVrfRoutingInstanceBuilder WithTenant(int? tenantId);
        IVrfRoutingInstanceBuilder WithRouteDistinguisherRange(RouteDistinguisherRangeTypeEnum? rdRangeType);
        IVrfRoutingInstanceBuilder WithRoutingInstanceType(RoutingInstanceTypeEnum routingInstanceTypeEnum);
    }
}
