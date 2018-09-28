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
    /// Builder for updating infrastructure devices - i.e. devices which form part of the infrastructure of the provider network. 
    /// Infrastructure devices are used to create the shared network core platform used by tenants.
    /// The builder exposes a fluent API.
    /// </summary>
    public class InfrastructureDeviceUpdateBuilder : InfrastructureDeviceBuilder, IInfrastructureDeviceUpdateBuilder
    {
        private readonly IPortUpdateDirector _portUpdateDirector;

        public InfrastructureDeviceUpdateBuilder(IUnitOfWork unitOfWork, IRoutingInstanceDirector routingInstanceDirector, 
            IPortDirector portDirector, IPortUpdateDirector portUpdateDirector) : base(unitOfWork, routingInstanceDirector, portDirector)
        {
            _portUpdateDirector = portUpdateDirector;
        }

        public IInfrastructureDeviceUpdateBuilder ForDevice(int? deviceId)
        {
            if (deviceId.HasValue) _args.Add(nameof(ForDevice), deviceId);
            return this;
        }

        IInfrastructureDeviceUpdateBuilder IInfrastructureDeviceUpdateBuilder.UseLayer2InterfaceMtu(bool? useLayer2InterfaceMtu)
        {
            base.UseLayer2InterfaceMtu(useLayer2InterfaceMtu);
            return this;
        }

        IInfrastructureDeviceUpdateBuilder IInfrastructureDeviceUpdateBuilder.WithDescription(string description)
        {
            base.WithDescription(description);
            return this;
        }

        IInfrastructureDeviceUpdateBuilder IInfrastructureDeviceUpdateBuilder.WithName(string name)
        {
            base.WithName(name);
            return this;
        }

        IInfrastructureDeviceUpdateBuilder IInfrastructureDeviceUpdateBuilder.WithStatus(string status)
        {
            base.WithStatus(status);
            return this;
        }

        public IInfrastructureDeviceUpdateBuilder WithPorts(List<PortUpdate> ports)
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
