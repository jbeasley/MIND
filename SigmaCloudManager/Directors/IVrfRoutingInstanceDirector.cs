using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface IVrfRoutingInstanceDirector
    {
        Task<SCM.Models.RoutingInstance> BuildAsync(int deviceId, int? tenantId, RoutingInstanceRequest request = null);
        Task<SCM.Models.RoutingInstance> BuildAsync(Attachment attachment, RoutingInstanceRequest request = null);
        Task<SCM.Models.RoutingInstance> BuildAsync(Vif vif, RoutingInstanceRequest request = null);
        Task<SCM.Models.RoutingInstance> BuildAsync(int routingInstanceId, RoutingInstanceRequest request = null);
    }
}
