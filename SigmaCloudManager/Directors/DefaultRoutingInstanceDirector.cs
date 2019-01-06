using Mind.Models.RequestModels;
using SCM.Models;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class DefaultRoutingInstanceDirector : IRoutingInstanceDirector
    {
        private readonly IDefaultRoutingInstanceBuilder _builder;

        public DefaultRoutingInstanceDirector(IDefaultRoutingInstanceBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Build a new default routing instance for a given device.
        /// </summary>
        /// <returns>The routing instance</returns>
        /// <param name="device">Device.</param>
        /// <param name="request">Request.</param>
        public async Task<SCM.Models.RoutingInstance> BuildAsync(Device device, RoutingInstanceRequest request)
        {
            return await _builder.ForDevice(device)
                                 .WithBgpPeers(request.BgpPeers)
                                 .BuildAsync();
        }

        /// <summary>
        /// Updates an existing default routing instance
        /// </summary>
        /// <param name="routingInstanceId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SCM.Models.RoutingInstance> UpdateAsync(int routingInstanceId, RoutingInstanceRequest request)
        {
            return await _builder.ForRoutingInstance(routingInstanceId)
                                 .WithBgpPeers(request?.BgpPeers)
                                 .BuildAsync();
        }
    }
}
