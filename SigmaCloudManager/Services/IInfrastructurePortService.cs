using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using Mind.Models.RequestModels;
using SCM.Services;

namespace Mind.Services
{
    public interface IInfrastructurePortService : IBaseService
    { 
        Task<Port> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<Port>> GetAllByDeviceIDAsync(int deviceId, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<Port>> GetAllByInterfaceIDAsync(int interfaceId, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<Port>> GetAllByAttachmentIDAsync(int attachmentId, bool? deep = false, bool asTrackable = false);
        Task<Port> AddAsync(int deviceId, PortRequest request);
        Task<Port> UpdateAsync(int portId, PortUpdate update);
        Task DeleteAsync(int portId);
    }
}
