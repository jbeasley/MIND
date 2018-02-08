using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnTenantNetworkOutService
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<VpnTenantNetworkOut>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkOut>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkOut>> GetAllByTenantNetworkIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkOut>> GetAllByVpnIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkOut>> GetAllByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true);
        Task<VpnTenantNetworkOut> GetByIDAsync(int id, bool includeProperties = true);
        Task<VpnTenantNetworkOut> GetOneAsync(int attachmentSetID, int tenantNetworkID, bool includeProperties = true);
        Task<int> AddAsync(VpnTenantNetworkOut vpnTenantNetworkOut);
        Task<int> UpdateAsync(VpnTenantNetworkOut vpnTenantNetworkOut);
        Task<int> DeleteAsync(VpnTenantNetworkOut vpnTenantNetworkOut);
    }
}
