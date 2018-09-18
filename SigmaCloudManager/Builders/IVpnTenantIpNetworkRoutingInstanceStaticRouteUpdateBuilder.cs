using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder
    {
        IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder ForVpnTenantIpNetworkRoutingInstanceStaticRoute(int? vpnTenantIpNetworkRoutingInstanceStaticRouteId);
        IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder WithIpv4NextHopAddress(string ipv4NextHopAddress);
        IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder AddToAllRoutingInstancesInAttachmentSet(bool? addToAllRoutingInstancesInAttachmentSet);
        IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder WithRoutingInstance(string routingInstanceName);
        IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder WithBfd(bool? isBfdEnabled);
        Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> UpdateAsync();
    }
}
