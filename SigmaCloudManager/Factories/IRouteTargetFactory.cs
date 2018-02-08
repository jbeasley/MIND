using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Factories
{
    public interface IRouteTargetFactory
    {
        Task<FactoryResult> NewAsync(RouteTargetRequest request);
        Task<FactoryResult> NewForVpnAsync(RouteTargetRequest request);
    }
}
