using Microsoft.EntityFrameworkCore;
using SCM.Data;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for creating a static routes within a routing instance
    /// The builder exposes a fluent UI
    /// </summary>
    public class RoutingInstanceStaticRouteBuilder : BaseBuilder, IRoutingInstanceStaticRouteBuilder
    {
        protected VpnTenantIpNetworkRoutingInstanceStaticRoute _vpnTenantIpNetworkRoutingInstanceStaticRoute;

        public RoutingInstanceStaticRouteBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _vpnTenantIpNetworkRoutingInstanceStaticRoute = new VpnTenantIpNetworkRoutingInstanceStaticRoute();
        }

        public virtual IRoutingInstanceStaticRouteBuilder ForAttachmentSet(int? attachmentSetId)
        {
            if (attachmentSetId != null) _args.Add(nameof(ForAttachmentSet), attachmentSetId);
            return this;
        }

        public virtual IRoutingInstanceStaticRouteBuilder ForDevice(int? deviceId)
        {
            if (deviceId != null) _args.Add(nameof(ForDevice), deviceId);
            return this;
        }

        public virtual IRoutingInstanceStaticRouteBuilder ForRoutingInstanceStaticRoute(int? vpnTenantIpNetworkRoutingInstanceStaticRouteId)
        {
            if (vpnTenantIpNetworkRoutingInstanceStaticRouteId != null) _args.Add(nameof(ForRoutingInstanceStaticRoute), vpnTenantIpNetworkRoutingInstanceStaticRouteId);
            return this;
        }

        /// <summary>
        /// The owning tenant of the tenant IP network. Multiple tenants may be allocated the same CIDR block so the 
        /// tenant owner is required in order to identify the correct CIDR block.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public virtual IRoutingInstanceStaticRouteBuilder WithTenantOwner(int? tenantId)
        {
            if (tenantId != null) _args.Add(nameof(WithTenantOwner), tenantId);
            return this;
        }

        public virtual IRoutingInstanceStaticRouteBuilder WithTenantIpNetworkCidrName(string tenantIpNetworkCidrName)
        {
            if (!string.IsNullOrEmpty(tenantIpNetworkCidrName)) _args.Add(nameof(WithTenantIpNetworkCidrName), tenantIpNetworkCidrName);
            return this;
        }

        public virtual IRoutingInstanceStaticRouteBuilder AddToAllRoutingInstancesInAttachmentSet(bool? addToAllRoutingInstancesInAttachmentSet)
        {
            if (addToAllRoutingInstancesInAttachmentSet.HasValue) _args.Add(nameof(AddToAllRoutingInstancesInAttachmentSet), addToAllRoutingInstancesInAttachmentSet);
            return this;
        }

        public virtual IRoutingInstanceStaticRouteBuilder WithRoutingInstance(string routingInstanceName)
        {
            if (!string.IsNullOrEmpty(routingInstanceName)) _args.Add(nameof(WithRoutingInstance), routingInstanceName);
            return this;
        }

        public virtual IRoutingInstanceStaticRouteBuilder WithDefaultRoutingInstance()
        {
            _args.Add(nameof(WithDefaultRoutingInstance), true);
            return this;
        }

        public virtual IRoutingInstanceStaticRouteBuilder WithIpv4NextHopAddress(string ipv4NextHopAddress)
        {
            if (!string.IsNullOrEmpty(ipv4NextHopAddress)) _args.Add(nameof(WithIpv4NextHopAddress), ipv4NextHopAddress);
            return this;
        }

        public virtual IRoutingInstanceStaticRouteBuilder WithBfd(bool? isBfdEnabled)
        {
            if (isBfdEnabled.HasValue) _args.Add(nameof(WithBfd), isBfdEnabled);
            return this;
        }

        public async Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForAttachmentSet)))
            {
                await SetAttachmentSetAsync();
                if (_args.ContainsKey(nameof(WithRoutingInstance))) await SetRoutingInstanceForAttachmentSetAsync();
                if (_args.ContainsKey(nameof(AddToAllRoutingInstancesInAttachmentSet))) SetAddToAllRoutingInstancesInAttachmentSet();
            }

            if (_args.ContainsKey(nameof(ForDevice)))
            {
                if (_args.ContainsKey(nameof(WithRoutingInstance))) await SetRoutingInstanceForDeviceAsync();
                if (_args.ContainsKey(nameof(WithDefaultRoutingInstance))) await SetDefaultRoutingInstanceForDeviceAsync();
            }

            if (_args.ContainsKey(nameof(WithTenantIpNetworkCidrName)) && _args.ContainsKey(nameof(WithTenantOwner))) await SetTenantIpNetworkAsync();
            if (_args.ContainsKey(nameof(WithIpv4NextHopAddress))) SetIpv4NextHopAddress();

            _vpnTenantIpNetworkRoutingInstanceStaticRoute.Validate();
            return _vpnTenantIpNetworkRoutingInstanceStaticRoute;
        }

        protected virtual internal async Task SetAttachmentSetAsync()
        {
            var attachmentSetId = (int)_args[nameof(ForAttachmentSet)];
            var attachmentSet = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                            q =>
                                 q.AttachmentSetID == attachmentSetId,
                                 query: q => q.IncludeValidationProperties(),
                                 AsTrackable: true)
                                 select result)
                                 .SingleOrDefault();

            _vpnTenantIpNetworkRoutingInstanceStaticRoute.AttachmentSet = attachmentSet;
        }

        protected virtual internal async Task SetTenantIpNetworkAsync()
        {
            var tenantIpNetworkCidrName = _args[nameof(WithTenantIpNetworkCidrName)].ToString();
            var tenantOwnerId = (int)_args[nameof(WithTenantOwner)];
            var tenantIpNetwork = (from result in await _unitOfWork.TenantIpNetworkRepository.GetAsync(
                              q =>
                                   q.TenantID == tenantOwnerId &&
                                   q.CidrNameIncludingIpv4LessThanOrEqualToLength == tenantIpNetworkCidrName,
                                   AsTrackable: true)
                                   select result)
                                   .SingleOrDefault();

            _vpnTenantIpNetworkRoutingInstanceStaticRoute.TenantIpNetwork = tenantIpNetwork ?? throw new BuilderBadArgumentsException("Tenant IP network " +
                $"'{tenantIpNetworkCidrName}' was not found.");
        }

        private async Task SetRoutingInstanceStaticRouteAsync()
        {
            var vpnTenantIpNetworkRoutingInstanceStaticRouteId = (int)_args[nameof(ForRoutingInstanceStaticRoute)];
            var vpnTenantIpNetworkRoutingInstanceStaticRoute = (from result in await _unitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.GetAsync(
                                                             q =>
                                                                q.VpnTenantIpNetworkRoutingInstanceStaticRouteID == vpnTenantIpNetworkRoutingInstanceStaticRouteId,
                                                                query: q => q.IncludeValidationProperties(),
                                                                AsTrackable: true)
                                                                select result)
                                                                .SingleOrDefault();

            _vpnTenantIpNetworkRoutingInstanceStaticRoute = vpnTenantIpNetworkRoutingInstanceStaticRoute ??
                throw new BuilderBadArgumentsException($"Could not found the routing instance static route with ID '{vpnTenantIpNetworkRoutingInstanceStaticRouteId}'.");

        }

        protected virtual internal void SetAddToAllRoutingInstancesInAttachmentSet()
        {
            var addToAllRoutingInstancesInAttachmentSet = (bool)_args[nameof(AddToAllRoutingInstancesInAttachmentSet)];
            _vpnTenantIpNetworkRoutingInstanceStaticRoute.AddToAllRoutingInstancesInAttachmentSet = addToAllRoutingInstancesInAttachmentSet;
        }

        protected virtual internal void SetIpv4NextHopAddress()
        {
            var ipv4NextHopAddress = (string)_args[nameof(WithIpv4NextHopAddress)].ToString();
            _vpnTenantIpNetworkRoutingInstanceStaticRoute.Ipv4NextHopAddress = ipv4NextHopAddress;
        }

        protected virtual internal async Task SetRoutingInstanceForAttachmentSetAsync()
        {
            var routingInstanceName = _args[nameof(WithRoutingInstance)].ToString();
            var routingInstance = (from routingInstances in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                                        x =>
                                           x.Name == routingInstanceName &&
                                           x.TenantID == _vpnTenantIpNetworkRoutingInstanceStaticRoute.TenantIpNetwork.TenantID,
                                           AsTrackable: true)
                                           select routingInstances)
                                           .SingleOrDefault();

            _vpnTenantIpNetworkRoutingInstanceStaticRoute.RoutingInstance = routingInstance;
        }

        protected virtual internal async Task SetDefaultRoutingInstanceForDeviceAsync()
        {
            var deviceId = (int)_args[nameof(ForDevice)];
            var routingInstance = (from routingInstances in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                                        x =>
                                           x.DeviceID == deviceId &&
                                           x.RoutingInstanceType.IsDefault,
                                           AsTrackable: true)
                                   select routingInstances)
                                           .SingleOrDefault();

            _vpnTenantIpNetworkRoutingInstanceStaticRoute.RoutingInstance = routingInstance;
        }

        protected virtual internal async Task SetRoutingInstanceForDeviceAsync()
        {
            var deviceId = (int)_args[nameof(ForDevice)];
            var routingInstanceName = _args[nameof(WithRoutingInstance)].ToString();
            var routingInstance = (from routingInstances in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                                        x =>
                                           x.DeviceID == deviceId &&
                                           x.Name == routingInstanceName,
                                           AsTrackable: true)
                                   select routingInstances)
                                           .SingleOrDefault();

            _vpnTenantIpNetworkRoutingInstanceStaticRoute.RoutingInstance = routingInstance;
        }
    }
}

