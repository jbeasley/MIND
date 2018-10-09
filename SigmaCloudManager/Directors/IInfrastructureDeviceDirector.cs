using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IInfrastructureDeviceDirector
    {
        Task<Device> BuildAsync(InfrastructureDeviceRequest request);
        Task<List<Device>> BuildAsync(List<InfrastructureDeviceRequest> requests);
        Task<Device> UpdateAsync(int deviceId, InfrastructureDeviceUpdate update);
        Task<List<Device>> UpdateAsync(List<InfrastructureDeviceUpdate> updates);
    }
}
