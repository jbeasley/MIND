using SCM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IBgpPeerDirector
    {
        Task<BgpPeer> BuildForDeviceAsync(int deviceId, BgpPeerRequest request);
        Task<BgpPeer> BuildForRoutingInstanceAsync(int routingInstanceId, BgpPeerRequest request);
        Task<BgpPeer> BuildAsync(RoutingInstance routingInstance, BgpPeerRequest request);
        Task<List<BgpPeer>> BuildAsync(RoutingInstance routingInstance, List<BgpPeerRequest> requests);
        Task<BgpPeer> UpdateAsync(int bgpPeerId, BgpPeerRequest request);
        Task<List<BgpPeer>> UpdateAsync(List<BgpPeerRequest> requests);
    }
}
