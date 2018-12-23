using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IInfrastructureDeviceBuilder
    {
        IInfrastructureDeviceBuilder ForDevice(int? deviceId);
        IInfrastructureDeviceBuilder WithName(string name);
        IInfrastructureDeviceBuilder WithDescription(string description);
        IInfrastructureDeviceBuilder WithLocation(string locationName);
        IInfrastructureDeviceBuilder WithRole(string role);
        IInfrastructureDeviceBuilder WithModel(string model);
        IInfrastructureDeviceBuilder WithStatus(string status);
        IInfrastructureDeviceBuilder UseLayer2InterfaceMtu(bool? useLayer2InterfaceMtu);
        IInfrastructureDeviceBuilder WithPortRequests(List<PortRequest> ports);
        IInfrastructureDeviceBuilder WithPortUpdates(List<PortUpdate> ports);
        IInfrastructureDeviceBuilder WithPlane(string planeName);
        Task<Device> BuildAsync();
    }
}
