using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface ITenantDeviceUpdateBuilder
    {
        ITenantDeviceUpdateBuilder ForDevice(int? deviceId);
        ITenantDeviceUpdateBuilder WithName(string name);
        ITenantDeviceUpdateBuilder WithDescription(string description);
        ITenantDeviceUpdateBuilder WithStatus(string status);
        ITenantDeviceUpdateBuilder UseLayer2InterfaceMtu(bool? useLayer2InterfaceMtu);
        ITenantDeviceUpdateBuilder WithPorts(List<PortUpdate> ports);
        Task<Device> UpdateAsync();
    }
}
