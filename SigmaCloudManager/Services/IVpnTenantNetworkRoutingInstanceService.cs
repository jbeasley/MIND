using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnTenantNetworkRoutingInstanceService
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<VpnTenantNetworkRoutingInstance>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkRoutingInstance>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkRoutingInstance>> GetAllByTenantNetworkIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkRoutingInstance>> GetAllByVpnIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkRoutingInstance>> GetAllByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true);
        Task<VpnTenantNetworkRoutingInstance> GetByIDAsync(int id, bool includeProperties = true);
        Task<VpnTenantNetworkRoutingInstance> GetOneAsync(int attachmentSetID, int tenantNetworkID, bool includeProperties = true);
        Task<int> AddAsync(VpnTenantNetworkRoutingInstance vpnTenantNetworkRoutingInstance);
        Task<int> UpdateAsync(VpnTenantNetworkRoutingInstance vpnTenantNetworkRoutingInstance);
        Task<int> DeleteAsync(VpnTenantNetworkRoutingInstance vpnTenantNetworkRoutingInstance);
    }
}
