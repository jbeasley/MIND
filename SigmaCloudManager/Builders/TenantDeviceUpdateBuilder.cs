using Mind.Models.RequestModels;
using SCM.Data;
using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for updating tenant devices - i.e. devices which form part of the tenant network. 
    /// The builder exposes a fluent API.
    /// </summary>
    public class TenantDeviceUpdateBuilder : TenantDeviceBuilder, ITenantDeviceUpdateBuilder
    {
        private readonly IPortUpdateDirector _portUpdateDirector;

        public TenantDeviceUpdateBuilder(IUnitOfWork unitOfWork, IRoutingInstanceDirector routingInstanceDirector, 
            IPortDirector portDirector, IPortUpdateDirector portUpdateDirector) : base(unitOfWork, routingInstanceDirector, portDirector)
        {
            _portUpdateDirector = portUpdateDirector;
        }

        public ITenantDeviceUpdateBuilder ForDevice(int? deviceId)
        {
            if (deviceId.HasValue) _args.Add(nameof(ForDevice), deviceId);
            return this;
        }

        ITenantDeviceUpdateBuilder ITenantDeviceUpdateBuilder.UseLayer2InterfaceMtu(bool? useLayer2InterfaceMtu)
        {
            base.UseLayer2InterfaceMtu(useLayer2InterfaceMtu);
            return this;
        }

        ITenantDeviceUpdateBuilder ITenantDeviceUpdateBuilder.WithDescription(string description)
        {
            base.WithDescription(description);
            return this;
        }

        ITenantDeviceUpdateBuilder ITenantDeviceUpdateBuilder.WithName(string name)
        {
            base.WithName(name);
            return this;
        }

        ITenantDeviceUpdateBuilder ITenantDeviceUpdateBuilder.WithStatus(string status)
        {
            base.WithStatus(status);
            return this;
        }

        public ITenantDeviceUpdateBuilder WithPorts(List<PortUpdate> ports)
        {
            if (ports != null) _args.Add(nameof(WithPorts), ports);
            return this;
        }

        public async Task<Device> UpdateAsync()
        {
            if (_args.ContainsKey(nameof(ForDevice))) await SetDeviceAsync();
            if (_args.ContainsKey(nameof(WithName))) SetName();
            if (_args.ContainsKey(nameof(WithStatus))) await SetStatusAsync();
            if (_args.ContainsKey(nameof(WithDescription))) SetDescription();
            if (_args.ContainsKey(nameof(UseLayer2InterfaceMtu))) SetUseLayer2InterfaceMtu();
            if (_args.ContainsKey(nameof(WithPorts))) await UpdatePortsAsync();
            _device.Validate();

            return _device;
        }

        private async Task SetDeviceAsync()
        {
            var deviceId = (int)_args[nameof(ForDevice)];
            var device = (from result in await _unitOfWork.DeviceRepository.GetAsync(
                        q =>
                          q.DeviceID == deviceId,
                          query: q => q.IncludeValidationProperties(),
                          AsTrackable: true)
                          select result)
                          .SingleOrDefault();

            base._device = device ?? throw new BuilderBadArgumentsException($"The device with ID '{deviceId}' was not found.");
        }

        private async Task UpdatePortsAsync()
        {
            var ports = (List<PortUpdate>)_args[nameof(WithPorts)];
            await _portUpdateDirector.UpdateAsync(ports);
        }
    }
}
