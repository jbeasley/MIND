using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IIpVpnUpdateBuilder
    {
        IIpVpnUpdateBuilder ForVpn(int? vpnId);
        IIpVpnUpdateBuilder WithName(string name);
        IIpVpnUpdateBuilder WithDescription(string description);
        IIpVpnUpdateBuilder WithRegion(string regionName);
        IIpVpnUpdateBuilder WithTenancyType(string tenancyName);
        IIpVpnUpdateBuilder WithExtranet(bool? isExtranet);
        IIpVpnUpdateBuilder WithMulticastVpnDirectionType(string multicastVpnDirectionType);
        Task<Vpn> UpdateAsync();
    }
}
