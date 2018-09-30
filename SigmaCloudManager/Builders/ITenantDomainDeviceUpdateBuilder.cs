using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface ITenantDomainDeviceUpdateBuilder
    {
        ITenantDomainDeviceUpdateBuilder ForDevice(int? deviceId);
        ITenantDomainDeviceUpdateBuilder WithName(string name);
        ITenantDomainDeviceUpdateBuilder WithDescription(string description);
        ITenantDomainDeviceUpdateBuilder WithStatus(string status);
        ITenantDomainDeviceUpdateBuilder UseLayer2InterfaceMtu(bool? useLayer2InterfaceMtu);
        ITenantDomainDeviceUpdateBuilder WithPorts(List<PortUpdate> ports);
        Task<Device> UpdateAsync();
    }
}
