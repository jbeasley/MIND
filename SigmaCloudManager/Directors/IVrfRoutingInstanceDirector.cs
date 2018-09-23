using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface IVrfRoutingInstanceDirector
    {
        Task<SCM.Models.RoutingInstance> BuildAsync(int deviceId, int tenantId, 
            RouteDistinguisherRangeTypeEnum rdRangeType = RouteDistinguisherRangeTypeEnum.Default);
    }
}
