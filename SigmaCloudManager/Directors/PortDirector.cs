using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class PortDirector : IPortDirector
    {
        private readonly IPortBuilder _builder;

        public PortDirector(IPortBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.Port> BuildAsync(int deviceId, PortRequest request)
        {
            return await _builder.ForDevice(deviceId)
                                 .WithType(request.Type)
                                 .WithName(request.Name)
                                 .WithPortBandwidth(request.PortBandwidthGbps)
                                 .WithConnector(request.PortConnector)
                                 .WithPortRole(request.PortRole)
                                 .WithPortPool(request.PortPool)
                                 .WithSfp(request.PortSfp)
                                 .WithStatus(request.PortStatus.ToString())
                                 .AssignToTenant(request.TenantId)
                                 .BuildAsync();
        }

        public async Task<SCM.Models.Port> BuildAsync(Device device, PortRequest request)
        {
            return await _builder.ForDevice(device)
                                 .WithType(request.Type)
                                 .WithName(request.Name)
                                 .WithPortBandwidth(request.PortBandwidthGbps)
                                 .WithConnector(request.PortConnector)
                                 .WithPortRole(request.PortRole)
                                 .WithPortPool(request.PortPool)
                                 .WithSfp(request.PortSfp)
                                 .WithStatus(request.PortStatus.ToString())
                                 .AssignToTenant(request.TenantId)
                                 .BuildAsync();
        }
    }
}
