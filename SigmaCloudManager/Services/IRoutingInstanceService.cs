using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using SCM.Models.RequestModels;

namespace SCM.Services
{
    public interface IRoutingInstanceService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<RoutingInstance> GetByIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<RoutingInstance>> GetAllByNameAsync(string name, bool includeProperties = true);
        Task<IEnumerable<RoutingInstance>> GetAllByRouteDistinguisherRangeNameAsync(string routeDistinguisherRangeName, bool includeProperties = true);
        Task<IEnumerable<RoutingInstance>> GetAllByDeviceIDAsync(int deviceID, int? tenantID = null, bool? isDefault = null, 
            bool? isLayer3 = null, bool? isTenantFacingVrf = null, bool? isInfrastructureVrf = false, bool includeProperties = false);
        Task<IEnumerable<RoutingInstance>> GetAllByAttachmentSetIDAsync(int attachmentSetID, bool includeProperties = true);
        Task<int> UpdateAsync(RoutingInstance routingInstance);
    }
}
