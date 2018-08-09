using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface IRoutingInstanceBuilder
    {
        void Init(int tenantId, int deviceId, int routingInstanceTypeId);
        Task Create();
        RoutingInstance GetResult();
    }
}
