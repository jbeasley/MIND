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
    /// Builder for infrastructure devices - i.e. devices which form part of the infrastructure of the provider network. 
    /// Infrastructure devices are used to create the shared network core platform used by tenants.
    /// The builder exposes a fluent API.
    /// </summary>
    public class InfrastructureDeviceBuilder : DeviceBuilder, IInfrastructureDeviceBuilder
    {
        public InfrastructureDeviceBuilder(IUnitOfWork unitOfWork, IRoutingInstanceDirector routingInstanceDirector, IPortDirector portDirector) :
            base(unitOfWork, routingInstanceDirector, portDirector)
        {
        }

        public IInfrastructureDeviceBuilder WithPlane(string planeName)
        {
            if (!string.IsNullOrEmpty(planeName)) _args.Add(nameof(WithPlane), planeName);
            return this;
        }

        IInfrastructureDeviceBuilder IInfrastructureDeviceBuilder.ForDevice(int? deviceId)
        {
            base.ForDevice(deviceId);
            return this;
        }

        IInfrastructureDeviceBuilder IInfrastructureDeviceBuilder.UseLayer2InterfaceMtu(bool? useLayer2InterfaceMtu)
        {
            base.UseLayer2InterfaceMtu(useLayer2InterfaceMtu);
            return this;
        }

        IInfrastructureDeviceBuilder IInfrastructureDeviceBuilder.WithDescription(string description)
        {
            base.WithDescription(description);
            return this;
        }

        IInfrastructureDeviceBuilder IInfrastructureDeviceBuilder.WithLocation(string locationName)
        {
            base.WithLocation(locationName);
            return this;
        }

        IInfrastructureDeviceBuilder IInfrastructureDeviceBuilder.WithModel(string model)
        {
            base.WithModel(model);
            return this;
        }

        IInfrastructureDeviceBuilder IInfrastructureDeviceBuilder.WithName(string name)
        {
            base.WithName(name);
            return this;
        }

        IInfrastructureDeviceBuilder IInfrastructureDeviceBuilder.WithPorts(List<PortRequest> ports)
        {
            base.WithPorts(ports);
            return this;
        }

        IInfrastructureDeviceBuilder IInfrastructureDeviceBuilder.WithPorts(List<PortUpdate> ports)
        {
            base.WithPorts(ports);
            return this;
        }

        IInfrastructureDeviceBuilder IInfrastructureDeviceBuilder.WithRole(string role)
        {
            base.WithRole(role);
            return this;
        }

        IInfrastructureDeviceBuilder IInfrastructureDeviceBuilder.WithStatus(string status)
        {
            base.WithStatus(status);
            return this;
        }

        public override async Task<Device> BuildAsync()
        {
            await base.BuildAsync();
            if (_args.ContainsKey(nameof(WithPlane))) await SetPlaneAsync();
            _device.Validate();

            return _device;
        }

        protected override internal async Task SetRoleAsync()
        {
            var roleName = _args[nameof(WithRole)].ToString();
            var role = (from result in await _unitOfWork.DeviceRoleRepository.GetAsync(
                      x =>
                        x.Name == roleName && x.IsProviderDomainRole,
                        AsTrackable: true)
                        select result)
                        .SingleOrDefault();

            _device.DeviceRole = role ?? throw new BuilderBadArgumentsException($"The device role with name '{roleName}' was not found or is invalid " +
                $"for an infrastructure device.");
        }

        protected internal virtual async Task SetPlaneAsync()
        {
            var planeName = _args[nameof(WithPlane)].ToString();
            var plane = (from result in await _unitOfWork.PlaneRepository.GetAsync(
                    q =>
                        q.Name == planeName,
                         AsTrackable: true)
                         select result)
                        .SingleOrDefault();

            _device.Plane = plane;
        }
    }
}
