using Mind.Models.RequestModels;
using SCM.Models;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface IRoutingInstanceDirector
    {
        Task<SCM.Models.RoutingInstance> BuildAsync(Device device, RoutingInstanceRequest request);
        Task<SCM.Models.RoutingInstance> UpdateAsync(int routingInstanceId, RoutingInstanceRequest request);
    }
}
