using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDeviceDirector : ITenantDeviceDirector
    {
        private readonly ITenantDeviceBuilder _builder;

        public TenantDeviceDirector(ITenantDeviceBuilder builder)
        {
            _builder = builder;
        }
        public async Task<Device> BuildAsync(int tenantId, TenantDeviceRequest request)
        {
            return await _builder.ForTenant(tenantId)
                                 .WithName(request.Name)
                                 .WithDescription(request.Description)
                                 .WithLocation(request.LocationName)
                                 .WithModel(request.DeviceModel)
                                 .WithRole(request.DeviceRole)
                                 .WithStatus(request.DeviceStatus.ToString())
                                 .UseLayer2InterfaceMtu(request.UseLayer2InterfaceMtu)
                                 .WithPorts(request.Ports)
                                 .BuildAsync();
        }
    }
}
