using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using Mind.Models.RequestModels;

namespace SCM.Services
{
    public interface IVpnTenantIpNetworkInService : IBaseService
    {
        Task<IEnumerable<VpnTenantIpNetworkIn>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkIn> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<VpnTenantIpNetworkIn>> GetAllByVpnIDAsync(int vpnId, int? tenantId = null, bool extranet = false, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkIn> AddAsync(VpnTenantIpNetworkIn vpnTenantIpNetworkIn);
        Task<VpnTenantIpNetworkIn> UpdateAsync(VpnTenantIpNetworkIn vpnTenantIpNetworkIn);
        Task DeleteAsync(int vpnTenantIpNetworkInId);
    }
}
