using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class RoutingInstanceStaticRouteDirector : IRoutingInstanceStaticRouteDirector
    {
        private readonly IRoutingInstanceStaticRouteBuilder _builder;

        public RoutingInstanceStaticRouteDirector(IRoutingInstanceStaticRouteBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.VpnTenantIpNetworkRoutingInstanceStaticRoute> BuildAsync(int attachmentSetId, VpnTenantIpNetworkRoutingInstanceStaticRouteRequest request)
        {
            return await _builder.ForAttachmentSet(attachmentSetId)
                                 .WithTenantOwner(request.TenantId)
                                 .WithIpv4NextHopAddress(request.Ipv4NextHopAddress)
                                 .WithTenantIpNetworkCidrName(request.TenantIpNetworkCidrName)
                                 .AddToAllRoutingInstancesInAttachmentSet(request.AddToAllRoutingInstancesInAttachmentSet)
                                 .WithRoutingInstance(request.RoutingInstanceName)
                                 .BuildAsync();
        }
    }
}
