using System.Collections.Generic;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Services;

namespace Mind.Services
{
    public interface IInfrastructureRoutingInstanceService : IBaseService
    {
        Task<RoutingInstance> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<RoutingInstance>> GetAllByDeviceIDAsync(int deviceId, bool? deep = false, bool asTrackable = false);
        Task<RoutingInstance> UpdateAsync(int routingInstanceId, RoutingInstanceRequest update);
    }
}
