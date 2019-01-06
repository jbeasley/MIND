using Mind.Models.RequestModels;
using SCM.Models;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface IVrfRoutingInstanceDirector : IRoutingInstanceDirector
    {
        Task<SCM.Models.RoutingInstance> BuildAsync(int deviceId, int? tenantId, RoutingInstanceRequest request = null);
        Task<SCM.Models.RoutingInstance> BuildAsync(Attachment attachment, RoutingInstanceRequest request = null);
        Task<SCM.Models.RoutingInstance> BuildAsync(Vif vif, RoutingInstanceRequest request = null);
    }
}
