using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IVpnDirector
    {
        Task<Vpn> BuildAsync(int tenantId, VpnRequest request, bool syncToNetwork = false);
        Task<Vpn> UpdateAsync(int vpnId, VpnUpdate update, bool syncToNetworkPut = false, bool syncToNetworkPatch = false);
    }
}
