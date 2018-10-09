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
    /// Builder for tenant devices - i.e. devices which form part of the tenant network.
    /// The builder exposes a fluent API.
    /// </summary>
    public class TenantDomainDeviceBuilder : DeviceBuilder, ITenantDomainDeviceBuilder
    {
        public TenantDomainDeviceBuilder(IUnitOfWork unitOfWork, IRoutingInstanceDirector routingInstanceDirector, IPortDirector portDirector) :
            base(unitOfWork, routingInstanceDirector, portDirector)
        {
        }

        public virtual ITenantDomainDeviceBuilder ForTenant(int? tenantId)
        {
            if (tenantId.HasValue) _args.Add(nameof(ForTenant), tenantId);
            return this;
        }

        ITenantDomainDeviceBuilder ITenantDomainDeviceBuilder.ForDevice(int? deviceId)
        {
            base.ForDevice(deviceId);
            return this;
        }

        ITenantDomainDeviceBuilder ITenantDomainDeviceBuilder.UseLayer2InterfaceMtu(bool? useLayer2InterfaceMtu)
        {
            base.UseLayer2InterfaceMtu(useLayer2InterfaceMtu);
            return this;
        }

        ITenantDomainDeviceBuilder ITenantDomainDeviceBuilder.WithDescription(string description)
        {
            base.WithDescription(description);
            return this;
        }

        ITenantDomainDeviceBuilder ITenantDomainDeviceBuilder.WithLocation(string locationName)
        {
            base.WithLocation(locationName);
            return this;
        }

        ITenantDomainDeviceBuilder ITenantDomainDeviceBuilder.WithModel(string model)
        {
            base.WithModel(model);
            return this;
        }

        ITenantDomainDeviceBuilder ITenantDomainDeviceBuilder.WithName(string name)
        {
            base.WithName(name);
            return this;
        }

        ITenantDomainDeviceBuilder ITenantDomainDeviceBuilder.WithPorts(List<PortRequest> ports)
        {
            base.WithPorts(ports);
            return this;
        }

        ITenantDomainDeviceBuilder ITenantDomainDeviceBuilder.WithPorts(List<PortUpdate> ports)
        {
            base.WithPorts(ports);
            return this;
        }

        ITenantDomainDeviceBuilder ITenantDomainDeviceBuilder.WithRole(string role)
        {
            base.WithRole(role);
            return this;
        }

        ITenantDomainDeviceBuilder ITenantDomainDeviceBuilder.WithStatus(string status)
        {
            base.WithStatus(status);
            return this;
        }

        public override async Task<Device> BuildAsync()
        {
            await base.BuildAsync();
            if (_args.ContainsKey(nameof(ForTenant))) await SetTenantAsync();
            _device.Validate();

            return _device;
        }

        protected internal virtual async Task SetTenantAsync()
        {
            var tenantId = (int)_args[nameof(ForTenant)];
            var tenant = (from result in await _unitOfWork.TenantRepository.GetAsync(
                        q =>
                          q.TenantID == tenantId,
                          AsTrackable: true)
                          select result)
                          .SingleOrDefault();

            _device.Tenant = tenant ?? throw new BuilderBadArgumentsException($"The tenant with ID '{tenantId}' was not found.");
        }

        protected override internal async Task SetRoleAsync()
        {
            var roleName = _args[nameof(WithRole)].ToString();
            var role = (from result in await _unitOfWork.DeviceRoleRepository.GetAsync(
                      x =>
                        x.Name == roleName && x.IsTenantDomainRole,
                        AsTrackable: true)
                        select result)
                        .SingleOrDefault();

            _device.DeviceRole = role ?? throw new BuilderBadArgumentsException($"The device role with name '{roleName}' was not found or is invalid " +
                $"for a tenant device.");
        }
    }
}
