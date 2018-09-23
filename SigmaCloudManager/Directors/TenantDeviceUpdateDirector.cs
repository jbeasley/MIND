using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class TenantDeviceUpdateDirector : ITenantDeviceUpdateDirector
    {
        private readonly ITenantDeviceUpdateBuilder _builder;

        public TenantDeviceUpdateDirector(ITenantDeviceUpdateBuilder builder)
        {
            _builder = builder;
        }
        public async Task<Device> UpdateAsync(int deviceId, TenantDeviceUpdate update)
        {
            return await _builder.ForDevice(deviceId)
                                 .WithName(update.Name)
                                 .WithDescription(update.Description)                                                        
                                 .WithStatus(update.DeviceStatus.ToString())
                                 .UseLayer2InterfaceMtu(update.UseLayer2InterfaceMtu)
                                 .WithPorts(update.Ports)                              
                                 .UpdateAsync();
        }
    }
}
