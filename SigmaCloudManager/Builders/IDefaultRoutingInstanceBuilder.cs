using System.Collections.Generic;
using Mind.Models.RequestModels;
using SCM.Models;

namespace Mind.Builders
{
    public interface IDefaultRoutingInstanceBuilder : IRoutingInstanceBuilder
    {
        IDefaultRoutingInstanceBuilder ForDevice(Device device);
        IDefaultRoutingInstanceBuilder ForRoutingInstance(int? routingInstanceId);
        IDefaultRoutingInstanceBuilder WithBgpPeers(List<BgpPeerRequest> bgpPeerRequests);
    }
}
