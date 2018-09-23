using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface ITenantDeviceBuilder
    {
        ITenantDeviceBuilder ForTenant(int? tenantId);
        ITenantDeviceBuilder WithName(string name);
        ITenantDeviceBuilder WithDescription(string description);
        ITenantDeviceBuilder WithLocation(string locationName);
        ITenantDeviceBuilder WithRole(string role);
        ITenantDeviceBuilder WithModel(string model);
        ITenantDeviceBuilder WithStatus(string status);
        ITenantDeviceBuilder UseLayer2InterfaceMtu(bool? useLayer2InterfaceMtu);
        ITenantDeviceBuilder WithPorts(List<PortRequest> ports);
        Task<Device> BuildAsync();
    }
}
