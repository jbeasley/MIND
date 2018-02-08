using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnTenantCommunityOutService
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<VpnTenantCommunityOut>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<VpnTenantCommunityOut>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantCommunityOut>> GetAllByTenantCommunityIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantCommunityOut>> GetAllByVpnIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantCommunityOut>> GetAllByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true);
        Task<VpnTenantCommunityOut> GetByIDAsync(int id, bool includeProperties = true);
        Task<VpnTenantCommunityOut> GetOneAsync(int attachmentSetID, int tenantCommunityID, bool includeProperties = true);
        Task<int> AddAsync(VpnTenantCommunityOut vpnTenantCommunityOut);
        Task<int> UpdateAsync(VpnTenantCommunityOut vpnTenantCommunityOut);
        Task<int> DeleteAsync(VpnTenantCommunityOut vpnTenantCommunityOut);
    }
}
