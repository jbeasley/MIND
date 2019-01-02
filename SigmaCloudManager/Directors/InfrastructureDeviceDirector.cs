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
        // Factory for the device builder - the factory ensures we get a unique instance of the builder
        // for each device request which is necessary when constructing a collection of devices
        private readonly Func<IInfrastructureDeviceBuilder> _builderFactory;

        public InfrastructureDeviceDirector(Func<IInfrastructureDeviceBuilder> builderFactory)
        {
            _builderFactory = builderFactory;
        }

        public async Task<Device> BuildAsync(InfrastructureDeviceRequest request)
        {
            var builder = _builderFactory();
            return await builder.WithName(request.Name)
                                .WithDescription(request.Description)
                                .WithLocation(request.LocationName)
                                .WithModel(request.DeviceModel)
                                .WithRole(request.DeviceRole)
                                .WithStatus(request.DeviceStatus.ToString())
                                .UseLayer2InterfaceMtu(request.UseLayer2InterfaceMtu)
                                .WithPortRequestsOrUpdates(request.Ports)
                                .WithPlane(request.PlaneName.ToString())
                                .BuildAsync();
        }

        public async Task<List<Device>> BuildAsync(List<InfrastructureDeviceRequest> requests)
        {
            var devices = new List<Device>();
            var tasks = requests.Select(
                                 async request =>
                                 {
                                     // Each device will be built from a distinct instance of the device builder
                                     devices.Add(await BuildAsync(request));
                                 });

            await Task.WhenAll(tasks);
            return devices;
        }

        public async Task<Device> UpdateAsync(int deviceId, InfrastructureDeviceUpdate update)
        {
            var builder = _builderFactory();
            return await builder.ForDevice(deviceId)
                                .WithName(update.Name)
                                .WithDescription(update.Description)
                                .WithStatus(update.DeviceStatus.ToString())
                                .UseLayer2InterfaceMtu(update.UseLayer2InterfaceMtu)
                                .WithPortRequestsOrUpdates(update.Ports)
                                .BuildAsync();
        }

        public async Task<List<Device>> UpdateAsync(List<InfrastructureDeviceUpdate> updates)
        {
            var devices = new List<Device>();
            var tasks = updates.Select(
                                 async update =>
                                 {
                                     if (update.DeviceId.HasValue)
                                     {
                                         // Each device will be updated using a distinct instance of the device builder
                                         devices.Add(await UpdateAsync(update.DeviceId.Value, update));
                                     }
                                 });

            await Task.WhenAll(tasks);
            return devices;
        }
    }
}
