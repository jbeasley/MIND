using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;

namespace Mind.Builders
{
    public interface IVpnBuilder
    {
        IVpnBuilder ForTenant(int tenantId);
        IVpnBuilder WithName(string name);
        IVpnBuilder WithDescription(string description);
        IVpnBuilder WithRegion(string regionName);
        IVpnBuilder WithTopologyType(string topologyName);
        IVpnBuilder WithPlane(string planeName);
        IVpnBuilder WithTenancyType(string tenancyName);
        IVpnBuilder AsNovaVpn(bool? isNovaVpn);
        IVpnBuilder WithAddressFamily(string addressFamilyName);
        Task<Vpn> BuildAsync();
    }
}
