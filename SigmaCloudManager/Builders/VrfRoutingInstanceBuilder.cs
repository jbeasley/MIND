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
    /// Builder for vrf routing instances. The builder exposes a fluent API.
    /// </summary>
    public class VrfRoutingInstanceBuilder : BaseBuilder, IVrfRoutingInstanceBuilder
    {
        private readonly RoutingInstance _routingInstance;

        public VrfRoutingInstanceBuilder(IUnitOfWork unitOfWork) : base (unitOfWork)
        {
            _routingInstance = new RoutingInstance
            {
                Name = Guid.NewGuid().ToString("N")
            };
        }

        public virtual IVrfRoutingInstanceBuilder ForDevice(int deviceId)
        {
            _args.Add(nameof(ForDevice), deviceId);
            return this;
        }

        public virtual IVrfRoutingInstanceBuilder WithTenant(int? tenantId)
        {
            if (tenantId.HasValue) _args.Add(nameof(WithTenant), tenantId);
            return this;
        }

        public virtual IVrfRoutingInstanceBuilder WithRoutingInstanceType(RoutingInstanceTypeEnum routingInstanceTypeEnum)
        {
            _args.Add(nameof(WithRoutingInstanceType), routingInstanceTypeEnum);
            return this;
        }

        public virtual IVrfRoutingInstanceBuilder WithRouteDistinguisherRange(RouteDistinguisherRangeTypeEnum? rdRangeType)
        {
            _args.Add(nameof(WithRouteDistinguisherRange), rdRangeType);
            return this;
        }

        public virtual async Task<RoutingInstance> BuildAsync()
        {
            await SetDeviceAsync();
            if (_args.ContainsKey(nameof(WithTenant))) await SetTenantAsync();
            await SetRoutingInstanceTypeAsync();
            await SetRouteDistinguisherAsync();

            _routingInstance.Validate();
            return _routingInstance;
        }

        protected internal virtual async Task SetDeviceAsync()
        {
            var deviceId = (int)_args[nameof(ForDevice)];
            var device = (from result in await _unitOfWork.DeviceRepository.GetAsync(
                    q =>
                        q.DeviceID == deviceId,
                        AsTrackable: true)
                        select result)
                        .SingleOrDefault();

            _routingInstance.Device = device;      
        }

        protected internal virtual async Task SetTenantAsync()
        {
            var tenantId = (int)_args[nameof(WithTenant)];
            var tenant = (from result in await _unitOfWork.TenantRepository.GetAsync(
                     q =>
                          q.TenantID == tenantId,
                          AsTrackable: true)
                          select result)
                          .SingleOrDefault();

            _routingInstance.Tenant = tenant;
        }

        protected internal virtual async Task SetRoutingInstanceTypeAsync()
        {
            var routingInstanceTypeEnum = (RoutingInstanceTypeEnum)_args[nameof(WithRoutingInstanceType)];
            var routingInstanceType = (from result in await _unitOfWork.RoutingInstanceTypeRepository.GetAsync(
                                q =>
                                       q.Type == routingInstanceTypeEnum,
                                       AsTrackable: true)
                                       select result)
                                       .SingleOrDefault();

            _routingInstance.RoutingInstanceType = routingInstanceType;
        }

        protected internal virtual async Task SetRouteDistinguisherAsync()
        {
            var rdRangeType = (RouteDistinguisherRangeTypeEnum)_args[nameof(WithRouteDistinguisherRange)];
            var rdRange = (from result in await _unitOfWork.RouteDistinguisherRangeRepository.GetAsync(
                    q => 
                        q.Type == rdRangeType,
                        AsTrackable: true)
                        select result)
                        .SingleOrDefault();

            if (rdRange == null)
            {
                throw new BuilderUnableToCompleteException($"The route distinguisher range '{rdRangeType.ToString()}' was not found. " +
                    "Please contact your system administrator to resolve this issue.");
            }

            var usedRDs = (from result in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                        q =>
                           q.RouteDistinguisherRange.Type == rdRangeType,
                           AsTrackable: true)
                           select result.AssignedNumberSubField.Value)
                           .ToList();

            // Allocate a new unused RD from the RD range

            int? newRdAssignedNumberSubField = Enumerable.Range(rdRange.AssignedNumberSubFieldStart, rdRange.AssignedNumberSubFieldCount)
                           .Except(usedRDs).FirstOrDefault();

            if (newRdAssignedNumberSubField == null) throw new BuilderUnableToCompleteException("Failed to allocate a free route distinguisher. "
                    + "Please contact your system administrator, or try another range.");

            _routingInstance.AdministratorSubField = rdRange.AdministratorSubField;
            _routingInstance.AssignedNumberSubField = newRdAssignedNumberSubField.Value;
            _routingInstance.RouteDistinguisherRange = rdRange;
        }
    }
}
