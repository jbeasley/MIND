using Mind.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class IpVpnUpdateDirector : IVpnUpdateDirector
    {
        private readonly IIpVpnUpdateBuilder _builder;
        public IpVpnUpdateDirector(IIpVpnUpdateBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.Vpn> UpdateAsync(int vpnId, VpnUpdate update)
        {
            return await _builder.ForVpn(vpnId)
                                 .WithName(update.Name)
                                 .WithDescription(update.Description)
                                 .WithRegion(update.Region.ToString())
                                 .WithTenancyType(update.TenancyType.ToString())
                                 .WithExtranet(update.IsExtranet)
                                 .WithMulticastVpnDirectionType(update.MulticastVpnDirectionType.ToString())
                                 .UpdateAsync();
        }
    }
}
