using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IInfrastructureDeviceUpdateBuilder
    {
        IInfrastructureDeviceUpdateBuilder ForDevice(int? deviceId);
        IInfrastructureDeviceUpdateBuilder WithName(string name);
        IInfrastructureDeviceUpdateBuilder WithDescription(string description);
        IInfrastructureDeviceUpdateBuilder WithStatus(string status);
        IInfrastructureDeviceUpdateBuilder UseLayer2InterfaceMtu(bool? useLayer2InterfaceMtu);
        IInfrastructureDeviceUpdateBuilder WithPorts(List<PortUpdate> ports);
        Task<Device> UpdateAsync();
    }
}
