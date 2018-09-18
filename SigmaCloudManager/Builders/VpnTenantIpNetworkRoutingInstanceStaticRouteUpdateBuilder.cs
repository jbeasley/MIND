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
    /// Builder for updating a tenant IP network static route association with a routing instance of an attachment set.
    /// The builder exposes a fluent UI
    /// </summary>
    public class VpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder : VpnTenantIpNetworkRoutingInstanceStaticRouteBuilder, 
        IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder
    {
        public VpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public virtual IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder ForVpnTenantIpNetworkRoutingInstanceStaticRoute(int? vpnTenantIpNetworkRoutingInstanceStaticRouteId)
        {
            if (vpnTenantIpNetworkRoutingInstanceStaticRouteId.HasValue) _args.Add(nameof(ForVpnTenantIpNetworkRoutingInstanceStaticRoute), vpnTenantIpNetworkRoutingInstanceStaticRouteId);
            return this;
        }

        IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder.AddToAllRoutingInstancesInAttachmentSet(bool? addToAllRoutingInstancesInAttachmentSet)
        {
            base.AddToAllRoutingInstancesInAttachmentSet(addToAllRoutingInstancesInAttachmentSet);
            return this;
        }

        IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder.WithRoutingInstance(string routingInstanceName)
        {
            base.WithRoutingInstance(routingInstanceName);
            return this;
        }

        IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder.WithIpv4NextHopAddress(string ipv4NextHopAddress)
        {
            base.WithIpv4NextHopAddress(ipv4NextHopAddress);
            return this;
        }

        IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder.WithBfd(bool? isBfdEnabled)
        {
            base.WithBfd(isBfdEnabled);
            return this;
        }

        public async Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> UpdateAsync()
        {
            if (_args.ContainsKey(nameof(ForVpnTenantIpNetworkRoutingInstanceStaticRoute))) await SetTenantIpNetworkRoutingInstanceStaticRouteAsync();
            if (_args.ContainsKey(nameof(WithIpv4NextHopAddress))) SetIpv4NextHopAddress();
            if (_args.ContainsKey(nameof(AddToAllRoutingInstancesInAttachmentSet))) SetAddToAllRoutingInstancesInAttachmentSet();
            if (_args.ContainsKey(nameof(WithRoutingInstance))) await SetRoutingInstanceAsync();

            _vpnTenantIpNetworkRoutingInstanceStaticRoute.Validate();
            return _vpnTenantIpNetworkRoutingInstanceStaticRoute;
        }

        private async Task SetTenantIpNetworkRoutingInstanceStaticRouteAsync()
        {
            var vpnTenantIpNetworkRoutingInstanceStaticRouteId = (int)_args[nameof(ForVpnTenantIpNetworkRoutingInstanceStaticRoute)];
            var vpnTenantIpNetworkRoutingInstanceStaticRoute = (from result in await _unitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.GetAsync(
                                                             q =>
                                                                q.VpnTenantIpNetworkRoutingInstanceStaticRouteID == vpnTenantIpNetworkRoutingInstanceStaticRouteId,
                                                                AsTrackable: true)
                                                                select result)
                                                                .SingleOrDefault();

            base._vpnTenantIpNetworkRoutingInstanceStaticRoute = vpnTenantIpNetworkRoutingInstanceStaticRoute ?? 
                throw new BuilderBadArgumentsException($"Could not found the vpn routing instance static route with ID '{vpnTenantIpNetworkRoutingInstanceStaticRouteId}'.");

        }
    }
}

