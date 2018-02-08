using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnTenantNetworkInService
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<VpnTenantNetworkIn>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkIn>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkIn>> GetAllByTenantNetworkIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkIn>> GetAllByVpnIDAsync(int vpnID, int? tenantID = null, bool? extranet = null, bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkIn>> GetAllByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true);
        Task<VpnTenantNetworkIn> GetByIDAsync(int id, bool includeProperties = true);
        Task<VpnTenantNetworkIn> GetOneAsync(int attachmentSetID, int tenantNetworkID, bool includeProperties = true);
        Task<int> AddAsync(VpnTenantNetworkIn vpnTenantNetworkIn);
        Task<int> UpdateAsync(VpnTenantNetworkIn vpnTenantNetworkIn);
        Task<int> DeleteAsync(VpnTenantNetworkIn vpnTenantNetworkIn);
    }
}
