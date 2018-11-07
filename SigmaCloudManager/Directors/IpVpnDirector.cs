using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class IpVpnDirector : IVpnDirector
    {
        private readonly IIpVpnBuilder _builder;
        public IpVpnDirector(IIpVpnBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.Vpn> BuildAsync(int tenantId, VpnRequest request)
        {
            return await _builder.ForTenant(tenantId)
                                .AsNovaVpn(request.IsNovaVpn)
                                .WithAddressFamily(request.AddressFamily.ToString())
                                .WithName(request.Name)
                                .WithDescription(request.Description)
                                .WithPlane(request.Plane.ToString())
                                .WithRegion(request.Region.ToString())
                                .WithRouteTargetRange(request.RouteTargetRange.ToString())
                                .WithRouteTargets(request.RouteTargetRequests)
                                .WithTenancyType(request.TenancyType.ToString())
                                .WithTopologyType(request.TopologyType.ToString()) 
                                .WithExtranet(request.IsExtranet)
                                .WithMulticast(request.IsMulticastVpn)
                                .WithMulticastVpnServiceType(request.MulticastVpnServiceType.ToString())
                                .WithMulticastVpnDirectionType(request.MulticastVpnDirectionType.ToString())
                                .WithAttachmentSets(request.VpnAttachmentSets)
                                .BuildAsync();
        }
    }
}
