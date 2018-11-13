using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IProviderDomainBgpPeerDirector
    {
        Task<BgpPeer> BuildAsync(int routingInstanceId, BgpPeerRequest request);
        Task<BgpPeer> BuildAsync(RoutingInstance routingInstance, BgpPeerRequest request);
        Task<List<BgpPeer>> BuildAsync(RoutingInstance routingInstance, List<BgpPeerRequest> requests);
    }
}
