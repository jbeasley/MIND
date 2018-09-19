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
    /// Builder for creating a static route for a tenant IP network association with a routing instance of an attachment set.
    /// The builder exposes a fluent UI
    /// </summary>
    public class VpnTenantIpNetworkRoutingInstanceStaticRouteBuilder : BaseBuilder, IVpnTenantIpNetworkRoutingInstanceStaticRouteBuilder
    {
        protected VpnTenantIpNetworkRoutingInstanceStaticRoute _vpnTenantIpNetworkRoutingInstanceStaticRoute;

        public VpnTenantIpNetworkRoutingInstanceStaticRouteBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _vpnTenantIpNetworkRoutingInstanceStaticRoute = new VpnTenantIpNetworkRoutingInstanceStaticRoute();
        }

        public virtual IVpnTenantIpNetworkRoutingInstanceStaticRouteBuilder ForAttachmentSet(int? attachmentSetId)
        {
            if (attachmentSetId != null) _args.Add(nameof(ForAttachmentSet), attachmentSetId);
            return this;
        }

        public virtual IVpnTenantIpNetworkRoutingInstanceStaticRouteBuilder WithTenantIpNetworkCidrName(string tenantIpNetworkCidrName)
        {
            if (!string.IsNullOrEmpty(tenantIpNetworkCidrName)) _args.Add(nameof(WithTenantIpNetworkCidrName), tenantIpNetworkCidrName);
            return this;
        }

        public virtual IVpnTenantIpNetworkRoutingInstanceStaticRouteBuilder AddToAllRoutingInstancesInAttachmentSet(bool? addToAllRoutingInstancesInAttachmentSet)
        {
            if (addToAllRoutingInstancesInAttachmentSet.HasValue) _args.Add(nameof(AddToAllRoutingInstancesInAttachmentSet), addToAllRoutingInstancesInAttachmentSet);
            return this;
        }

        public virtual IVpnTenantIpNetworkRoutingInstanceStaticRouteBuilder WithRoutingInstance(string routingInstanceName)
        {
            if (!string.IsNullOrEmpty(routingInstanceName)) _args.Add(nameof(WithRoutingInstance), routingInstanceName);
            return this;
        }

        public virtual IVpnTenantIpNetworkRoutingInstanceStaticRouteBuilder WithIpv4NextHopAddress(string ipv4NextHopAddress)
        {
            if (!string.IsNullOrEmpty(ipv4NextHopAddress)) _args.Add(nameof(WithIpv4NextHopAddress), ipv4NextHopAddress);
            return this;
        }

        public virtual IVpnTenantIpNetworkRoutingInstanceStaticRouteBuilder WithBfd(bool? isBfdEnabled)
        {
            if (isBfdEnabled.HasValue) _args.Add(nameof(WithBfd), isBfdEnabled);
            return this;
        }

        public async Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForAttachmentSet))) await SetAttachmentSetAsync();
            if (_args.ContainsKey(nameof(WithTenantIpNetworkCidrName))) await SetTenantIpNetworkAsync();
            if (_args.ContainsKey(nameof(WithIpv4NextHopAddress))) SetIpv4NextHopAddress();
            if (_args.ContainsKey(nameof(AddToAllRoutingInstancesInAttachmentSet))) SetAddToAllRoutingInstancesInAttachmentSet();
            if (_args.ContainsKey(nameof(WithRoutingInstance))) await SetRoutingInstanceAsync();

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
            var tenantIpNetwork = (from result in await _unitOfWork.TenantIpNetworkRepository.GetAsync(
                              q =>
                                   q.CidrNameIncludingIpv4LessThanOrEqualToLength == tenantIpNetworkCidrName,
                                   AsTrackable: true)
                                   select result)
                                   .SingleOrDefault();

            _vpnTenantIpNetworkRoutingInstanceStaticRoute.TenantIpNetwork = tenantIpNetwork ?? throw new BuilderBadArgumentsException("Tenant IP network " +
                $"'{tenantIpNetworkCidrName}' was not found.");
        }

        protected virtual internal void SetAddToAllRoutingInstancesInAttachmentSet()
        {
            var addToAllBgpPeersInAttachmentSet = (bool)_args[nameof(AddToAllRoutingInstancesInAttachmentSet)];
            _vpnTenantIpNetworkRoutingInstanceStaticRoute.AddToAllRoutingInstancesInAttachmentSet = addToAllBgpPeersInAttachmentSet;
        }

        protected virtual internal void SetIpv4NextHopAddress()
        {
            var ipv4NextHopAddress = (string)_args[nameof(WithIpv4NextHopAddress)].ToString();
            _vpnTenantIpNetworkRoutingInstanceStaticRoute.Ipv4NextHopAddress = ipv4NextHopAddress;
        }

        protected virtual internal async Task SetRoutingInstanceAsync()
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
    }
}

