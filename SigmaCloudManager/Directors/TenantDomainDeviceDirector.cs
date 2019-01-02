using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDomainDeviceDirector : ITenantDomainDeviceDirector
    {
        private readonly ITenantDomainDeviceBuilder _builder;

        public TenantDomainDeviceDirector(ITenantDomainDeviceBuilder builder)
        {
            _builder = builder;
        }

        public async Task<Device> BuildAsync(int tenantId, TenantDomainDeviceRequest request)
        {
            return await _builder.ForTenant(tenantId)
                                 .WithName(request.Name)
                                 .WithDescription(request.Description)
                                 .WithLocation(request.LocationName)
                                 .WithModel(request.DeviceModel)
                                 .WithRole(request.DeviceRole)
                                 .WithStatus(request.DeviceStatus.ToString())
                                 .UseLayer2InterfaceMtu(request.UseLayer2InterfaceMtu)
                                 .WithPortRequestsOrUpdates(request.Ports)
                                 .BuildAsync();
        }

        public async Task<Device> UpdateAsync(int deviceId, TenantDomainDeviceUpdate update)
        {
            return await _builder.ForDevice(deviceId)
                                 .WithName(update.Name)
                                 .WithDescription(update.Description)
                                 .WithStatus(update.DeviceStatus.ToString())
                                 .UseLayer2InterfaceMtu(update.UseLayer2InterfaceMtu)
                                 .WithPortRequestsOrUpdates(update.Ports)
                                 .BuildAsync();
        }
    }
}
