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
        Task<Port> BuildAsync(int deviceId, PortRequestOrUpdate request);
        Task<Port> BuildAsync(Device device, PortRequestOrUpdate request);
        Task<List<Port>> BuildAsync(Device device, List<PortRequestOrUpdate> requests);
        Task<Port> UpdateAsync(int portId, PortRequestOrUpdate update);
        Task<List<Port>> UpdateAsync(List<PortRequestOrUpdate> updates);
    }
}
