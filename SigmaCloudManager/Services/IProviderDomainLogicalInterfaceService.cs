using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using Mind.Models.RequestModels;
using SCM.Services;

namespace Mind.Services
{
    public interface IProviderDomainLogicalInterfaceService : IBaseService
    { 
        Task<LogicalInterface> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<LogicalInterface>> GetAllByRoutingInstanceIDAsync(int routingInstanceId, bool? deep = false, bool asTrackable = false);
        Task<LogicalInterface> AddAsync(int routingInstanceId, ProviderDomainLogicalInterfaceRequest request);
        Task<LogicalInterface> UpdateAsync(int logicalInterfaceId, LogicalInterfaceUpdate update);
        Task DeleteAsync(int logicalInterfaceId);
    }
}
