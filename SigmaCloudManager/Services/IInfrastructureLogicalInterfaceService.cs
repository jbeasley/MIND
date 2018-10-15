using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using Mind.Models.RequestModels;
using SCM.Services;

namespace Mind.Services
{
    public interface IInfrastructureLogicalInterfaceService : IBaseService
    { 
        Task<LogicalInterface> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<LogicalInterface>> GetAllByDeviceIDAsync(int deviceId, bool? deep = false, bool asTrackable = false);
        Task<LogicalInterface> AddAsync(int deviceId, InfrastructureLogicalInterfaceRequest request);
        Task<LogicalInterface> UpdateAsync(int logicalInterfaceId, LogicalInterfaceUpdate update);
        Task DeleteAsync(int logicalInterfaceId);
    }
}
