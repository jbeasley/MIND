using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnTenantCommunityRoutingInstanceService
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<VpnTenantCommunityRoutingInstance>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<VpnTenantCommunityRoutingInstance>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantCommunityRoutingInstance>> GetAllByTenantCommunityIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantCommunityRoutingInstance>> GetAllByTenantCommunitySetIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantCommunityRoutingInstance>> GetAllByVpnIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantCommunityRoutingInstance>> GetAllByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true);
        Task<VpnTenantCommunityRoutingInstance> GetByIDAsync(int id, bool includeProperties = true);
        Task<VpnTenantCommunityRoutingInstance> GetOneAsync(int attachmentSetID, int tenantCommunityID, bool includeProperties = true);
        Task<int> AddAsync(VpnTenantCommunityRoutingInstance vpnTenantCommunityRoutingInstance);
        Task<int> UpdateAsync(VpnTenantCommunityRoutingInstance vpnTenantCommunityRoutingInstance);
        Task<int> DeleteAsync(VpnTenantCommunityRoutingInstance vpnTenantCommunityRoutingInstance);
    }
}
