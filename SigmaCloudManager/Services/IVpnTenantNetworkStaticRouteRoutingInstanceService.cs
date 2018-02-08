using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnTenantNetworkStaticRouteRoutingInstanceService
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<VpnTenantNetworkStaticRouteRoutingInstance>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkStaticRouteRoutingInstance>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkStaticRouteRoutingInstance>> GetAllByTenantNetworkIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkStaticRouteRoutingInstance>> GetAllByVpnIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantNetworkStaticRouteRoutingInstance>> GetAllByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true);
        Task<VpnTenantNetworkStaticRouteRoutingInstance> GetByIDAsync(int id, bool includeProperties = true);
        Task<VpnTenantNetworkStaticRouteRoutingInstance> GetOneAsync(int attachmentSetID, int tenantNetworkID, bool includeProperties = true);
        Task<int> AddAsync(VpnTenantNetworkStaticRouteRoutingInstance vpnTenantNetworkStaticRouteRoutingInstance);
        Task<int> UpdateAsync(VpnTenantNetworkStaticRouteRoutingInstance vpnTenantNetworkStaticRouteRoutingInstance);
        Task<int> DeleteAsync(VpnTenantNetworkStaticRouteRoutingInstance vpnTenantNetworkStaticRouteRoutingInstance);
    }
}
