using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IPortDirector
    {
        Task<Port> BuildAsync(int deviceId, PortRequest request);
        Task<Port> BuildAsync(Device device, PortRequest request);
        Task<List<Port>> BuildAsync(Device device, List<PortRequest> requests);
        Task<Port> UpdateAsync(int portId, PortUpdate update);
        Task<List<Port>> UpdateAsync(List<PortUpdate> updates);
    }
}
