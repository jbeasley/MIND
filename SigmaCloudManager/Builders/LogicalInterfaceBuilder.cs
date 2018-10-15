using Microsoft.EntityFrameworkCore;
using Mind.Models.RequestModels;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for logical interfaces, e.g. loopbacks, tunnels etc
    /// The builder exposes a fluent API.
    /// </summary>
    public class LogicalInterfaceBuilder : BaseBuilder, ILogicalInterfaceBuilder
    {
        protected internal LogicalInterface _logicalInterface;

        public LogicalInterfaceBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _logicalInterface = new LogicalInterface();
        }

        public virtual ILogicalInterfaceBuilder ForDevice(int? deviceId)
        {
            if (deviceId.HasValue) _args.Add(nameof(ForDevice), deviceId);
            return this;
        }

        public virtual ILogicalInterfaceBuilder ForProviderDomainRoutingInstance(int? routingInstanceId)
        {
            if (routingInstanceId.HasValue) _args.Add(nameof(ForProviderDomainRoutingInstance), routingInstanceId);
            return this;
        }

        public virtual ILogicalInterfaceBuilder ForInfrastructureRoutingInstance(string routingInstanceName)
        {
            if (!string.IsNullOrEmpty(routingInstanceName)) _args.Add(nameof(ForInfrastructureRoutingInstance), routingInstanceName);
            return this;
        }

        public virtual ILogicalInterfaceBuilder ForLogicalInterface(int? logicalInterfaceId)
        {
            if (logicalInterfaceId.HasValue) _args.Add(nameof(ForLogicalInterface), logicalInterfaceId);
            return this;
        }

        public virtual ILogicalInterfaceBuilder WithDescription(string description)
        {
            if (!string.IsNullOrEmpty(description)) _args.Add(nameof(WithDescription), description);
            return this;
        }

        public virtual ILogicalInterfaceBuilder WithIpv4(Ipv4AddressAndMask ipv4AddressAndMask)
        {
            if (ipv4AddressAndMask != null) _args.Add(nameof(WithIpv4), ipv4AddressAndMask);
            return this;
        }

        public virtual ILogicalInterfaceBuilder WithType(string type)
        {
            if (!string.IsNullOrEmpty(type)) _args.Add(nameof(WithType), type);
            return this;
        }

        public async Task<LogicalInterface> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForLogicalInterface)))
            {
                await SetLogicalInterfaceAsync();
            }
            else if (_args.ContainsKey(nameof(ForDevice)))
            {
                if (_args.ContainsKey(nameof(ForInfrastructureRoutingInstance)))
                {
                    await SetInfrastructureRoutingInstanceAsync();
                }
                if (_args.ContainsKey(nameof(WithType))) SetType();
                SetID();
            }
            else if (_args.ContainsKey(nameof(ForProviderDomainRoutingInstance)))
            {
                await SetProviderDomainRoutingInstanceAsync();
                if (_args.ContainsKey(nameof(WithType))) SetType();
                SetID();
            }

            if (_args.ContainsKey(nameof(WithDescription))) SetDescription();
            if (_args.ContainsKey(nameof(WithIpv4))) SetIpv4();

            _logicalInterface.Validate();

            return _logicalInterface;
        }

        protected internal virtual async Task SetInfrastructureRoutingInstanceAsync()
        {
            var deviceId = (int)_args[nameof(ForDevice)];
            var routingInstanceName = _args.ContainsKey(nameof(ForInfrastructureRoutingInstance)) ? _args[nameof(ForInfrastructureRoutingInstance)].ToString() : null;
            var routingInstance = (from result in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                                q =>
                                   q.DeviceID == deviceId &&
                                   q.Device.DeviceRole.IsProviderDomainRole,
                                   query: q => q.Include(x => x.LogicalInterfaces)
                                                .Include(x => x.RoutingInstanceType),
                                   AsTrackable: true)
                                   select result)
                                  .Where(
                                        x =>
                                        {
                                            if (!string.IsNullOrEmpty(routingInstanceName))
                                            {
                                                return x.RoutingInstanceType.IsInfrastructureVrf && 
                                                       x.Name == routingInstanceName;
                                            }
                                            else
                                            {
                                                return x.RoutingInstanceType.IsDefault;
                                            }
                                        })
                                   .SingleOrDefault();

            _logicalInterface.RoutingInstance = routingInstance ?? 
                throw new BuilderBadArgumentsException($"The infrastructure routing instance for device with ID '{deviceId}' was not found.");
        }

        protected internal virtual async Task SetProviderDomainRoutingInstanceAsync()
        {
            var routingInstanceId = (int)_args[nameof(ForProviderDomainRoutingInstance)];
            var routingInstance = (from result in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                                q =>
                                   q.RoutingInstanceID == routingInstanceId &&
                                   q.RoutingInstanceType.IsTenantFacingVrf,
                                   query: q => q.Include(x => x.LogicalInterfaces)
                                                .Include(x => x.RoutingInstanceType),
                                   AsTrackable: true)
                                   select result)
                                   .SingleOrDefault();

            _logicalInterface.RoutingInstance = routingInstance ??
                throw new BuilderBadArgumentsException($"The provider domain routing instance with ID '{routingInstanceId}' was not found.");
        }

        protected internal virtual async Task SetLogicalInterfaceAsync()
        {
            var logicalInterfaceId = (int)_args[nameof(ForLogicalInterface)];
            var logicalInterface = (from result in await _unitOfWork.LogicalInterfaceRepository.GetAsync(
                                q =>
                                   q.LogicalInterfaceID == logicalInterfaceId,
                                   query: q => q.IncludeValidationProperties(),
                                   AsTrackable: true)
                                   select result)
                                  .SingleOrDefault();

            _logicalInterface = logicalInterface ??
                throw new BuilderBadArgumentsException($"The logical interface with ID '{logicalInterfaceId}' was not found.");
        }

        protected internal virtual void SetDescription()
        {
            var description = _args[nameof(WithDescription)].ToString();
            _logicalInterface.Description = description;
        }

        protected internal virtual void SetIpv4()
        {
            var ipv4 = (Ipv4AddressAndMask)_args[nameof(WithIpv4)];
            _logicalInterface.IpAddress = ipv4.IpAddress;
            _logicalInterface.SubnetMask = ipv4.SubnetMask;
        }

        protected internal virtual void SetType()
        {
            var typeName = _args[nameof(WithType)].ToString();
            _logicalInterface.LogicalInterfaceType = Enum.Parse<SCM.Models.LogicalInterfaceTypeEnum>(typeName);
        }

        protected internal virtual void SetID()
        {
            var usedIds = _logicalInterface.RoutingInstance.LogicalInterfaces
                                           .Where(
                                                x => 
                                                x.LogicalInterfaceType == _logicalInterface.LogicalInterfaceType)
                                           .Select(
                                                x => 
                                                x.ID);

            int? id = Enumerable.Range(1, 65535)
                                .Except(usedIds)
                                .FirstOrDefault();

            _logicalInterface.ID = id ?? throw new BuilderUnableToCompleteException("Unable to assign a free ID value to the Logical Interface. " +
                     "Please contact your administrator to report this issue.");
        }
    }
}
