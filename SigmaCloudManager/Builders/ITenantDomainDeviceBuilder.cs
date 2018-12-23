using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface ITenantDomainDeviceBuilder
    {
        ITenantDomainDeviceBuilder ForTenant(int? tenantId);
        ITenantDomainDeviceBuilder ForDevice(int? deviceId);
        ITenantDomainDeviceBuilder WithName(string name);
        ITenantDomainDeviceBuilder WithDescription(string description);
        ITenantDomainDeviceBuilder WithLocation(string locationName);
        ITenantDomainDeviceBuilder WithRole(string role);
        ITenantDomainDeviceBuilder WithModel(string model);
        ITenantDomainDeviceBuilder WithStatus(string status);
        ITenantDomainDeviceBuilder UseLayer2InterfaceMtu(bool? useLayer2InterfaceMtu);
        ITenantDomainDeviceBuilder WithPortRequests(List<PortRequest> ports);
        ITenantDomainDeviceBuilder WithPortUpdates(List<PortUpdate> ports);
        Task<Device> BuildAsync();
    }
}
