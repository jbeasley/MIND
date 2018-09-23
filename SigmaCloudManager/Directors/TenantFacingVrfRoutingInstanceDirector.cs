using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantFacingVrfRoutingInstanceDirector : IVrfRoutingInstanceDirector
    {
        private readonly IVrfRoutingInstanceBuilder _builder;

        public TenantFacingVrfRoutingInstanceDirector(IVrfRoutingInstanceBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.RoutingInstance> BuildAsync(int deviceId, int tenantId, RouteDistinguisherRangeTypeEnum rdRangeType = RouteDistinguisherRangeTypeEnum.Default)
        {
            return await _builder.ForDevice(deviceId)
                                 .WithTenant(tenantId)
                                 .WithRouteDistinguisherRange(rdRangeType)
                                 .WithRoutingInstanceType(RoutingInstanceTypeEnum.TenantFacingVrf)
                                 .BuildAsync();
        }
    }
}
