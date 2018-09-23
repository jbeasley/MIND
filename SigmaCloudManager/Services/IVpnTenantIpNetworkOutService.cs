using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using Mind.Models.RequestModels;

namespace SCM.Services
{
    public interface IVpnTenantIpNetworkOutService : IBaseService
    {
        Task<IEnumerable<VpnTenantIpNetworkOut>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<VpnTenantIpNetworkOut>> GetAllByVpnIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkOut> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkOut> AddAsync(VpnTenantIpNetworkOut vpnTenantNetworkOut);
        Task<VpnTenantIpNetworkOut> AddAsync(int attachmentSetId, VpnTenantIpNetworkOutRequest request);
        Task<VpnTenantIpNetworkOut> UpdateAsync(VpnTenantIpNetworkOut vpnTenantNetworkOut);
        Task<VpnTenantIpNetworkOut> UpdateAsync(int vpnTenantIpNetworkOutId, VpnTenantIpNetworkOutUpdate update);
        Task DeleteAsync(int vpnTenantNetworkOutId);
    }
}
