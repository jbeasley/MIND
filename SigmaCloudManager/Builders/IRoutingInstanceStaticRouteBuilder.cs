using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IRoutingInstanceStaticRouteBuilder
    {
        IRoutingInstanceStaticRouteBuilder ForAttachmentSet(int? attachmenSetId);
        IRoutingInstanceStaticRouteBuilder ForDevice(int? deviceId);
        IRoutingInstanceStaticRouteBuilder ForRoutingInstanceStaticRoute(int? vpnTenantIpNetworkRoutingInstanceStaticRouteId);
        IRoutingInstanceStaticRouteBuilder WithTenantOwner(int? tenantId);
        IRoutingInstanceStaticRouteBuilder WithTenantIpNetworkCidrName(string tenantIpNetworkCidrName);
        IRoutingInstanceStaticRouteBuilder WithIpv4NextHopAddress(string ipv4NextHopAddress);
        IRoutingInstanceStaticRouteBuilder AddToAllRoutingInstancesInAttachmentSet(bool? addToAllRoutingInstancesInAttachmentSet);
        IRoutingInstanceStaticRouteBuilder WithRoutingInstance(string routingInstanceName);
        IRoutingInstanceStaticRouteBuilder WithDefaultRoutingInstance();
        IRoutingInstanceStaticRouteBuilder WithBfd(bool? isBfdEnabled);
        Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> BuildAsync();
    }
}
