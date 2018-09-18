using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class VpnTenantIpNetworkRoutingInstanceStaticRouteUpdateDirector : IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateDirector
    {
        private readonly IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder _builder;

        public VpnTenantIpNetworkRoutingInstanceStaticRouteUpdateDirector(IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantIpNetworkRoutingInstanceStaticRoute> UpdateAsync(int vpnTenantIpNetworkRoutingInstanceStaticRouteId, VpnTenantIpNetworkRoutingInstanceStaticRouteUpdate update)
        {
            return await _builder.ForVpnTenantIpNetworkRoutingInstanceStaticRoute(vpnTenantIpNetworkRoutingInstanceStaticRouteId)
                                 .WithIpv4NextHopAddress(update.Ipv4NextHopAddress)
                                 .AddToAllRoutingInstancesInAttachmentSet(update.AddToAllRoutingInstancesInAttachmentSet)
                                 .WithRoutingInstance(update.RoutingInstanceName)
                                 .UpdateAsync();
        }
    }
}
