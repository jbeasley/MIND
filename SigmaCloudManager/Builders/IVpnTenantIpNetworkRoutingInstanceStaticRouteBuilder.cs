using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IVpnTenantIpNetworkRoutingInstanceStaticRouteBuilder
    {
        IVpnTenantIpNetworkRoutingInstanceStaticRouteBuilder ForAttachmentSet(int? attachmenSetId);
        IVpnTenantIpNetworkRoutingInstanceStaticRouteBuilder WithTenantIpNetworkCidrName(string tenantIpNetworkCidrName);
        IVpnTenantIpNetworkRoutingInstanceStaticRouteBuilder WithIpv4NextHopAddress(string ipv4NextHopAddress);
        IVpnTenantIpNetworkRoutingInstanceStaticRouteBuilder AddToAllRoutingInstancesInAttachmentSet(bool? addToAllRoutingInstancesInAttachmentSet);
        IVpnTenantIpNetworkRoutingInstanceStaticRouteBuilder WithRoutingInstance(string routingInstanceName);
        IVpnTenantIpNetworkRoutingInstanceStaticRouteBuilder WithBfd(bool? isBfdEnabled);
        Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> BuildAsync();
    }
}
