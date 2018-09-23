using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class InfrastructureDeviceDirector : IInfrastructureDeviceDirector
    {
        private readonly IInfrastructureDeviceBuilder _builder;

        public InfrastructureDeviceDirector(IInfrastructureDeviceBuilder builder)
        {
            _builder = builder;
        }
        public async Task<Device> BuildAsync(InfrastructureDeviceRequest request)
        {
            return await _builder.WithName(request.Name)
                                 .WithDescription(request.Description)
                                 .WithLocation(request.LocationName)
                                 .WithModel(request.DeviceModel)
                                 .WithRole(request.DeviceRole)
                                 .WithStatus(request.DeviceStatus.ToString())
                                 .UseLayer2InterfaceMtu(request.UseLayer2InterfaceMtu)
                                 .WithPorts(request.Ports)
                                 .WithPlane(request.PlaneName.ToString())
                                 .BuildAsync();
        }
    }
}
