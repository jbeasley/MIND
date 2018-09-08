using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IIpVpnBuilder
    {
        IIpVpnBuilder ForTenant(int tenantId);
        IIpVpnBuilder WithName(string name);
        IIpVpnBuilder WithDescription(string description);
        IIpVpnBuilder WithRegion(string regionName);
        IIpVpnBuilder WithTopologyType(string topologyName);
        IIpVpnBuilder WithPlane(string planeName);
        IIpVpnBuilder WithTenancyType(string tenancyName);
        IIpVpnBuilder AsNovaVpn(bool? isNovaVpn);
        IIpVpnBuilder WithAddressFamily(string addressFamilyName);
        IIpVpnBuilder WithRouteTargetRange(string rangeName);
        IIpVpnBuilder WithRouteTargets(List<RouteTargetRequest> routeTargetRequests);
        IIpVpnBuilder WithExtranet(bool? isExtranet);
        IIpVpnBuilder WithMulticast(bool? isMulticast);
        IIpVpnBuilder WithMulticastVpnServiceType(string multicastVpnServiceType);
        IIpVpnBuilder WithMulticastVpnDirectionType(string multicastVpnDirectionType);
        Task<Vpn> BuildAsync();
    }
}
