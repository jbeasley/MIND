using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnTenantCommunityInService
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<VpnTenantCommunityIn>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<VpnTenantCommunityIn>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantCommunityIn>> GetAllByTenantCommunityIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantCommunityIn>> GetAllByVpnIDAsync(int vpnID, int? tenantID = null, bool? extranet = null, bool includeProperties = true);
        Task<IEnumerable<VpnTenantCommunityIn>> GetAllByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true);
        Task<VpnTenantCommunityIn> GetByIDAsync(int id, bool includeProperties = true);
        Task<VpnTenantCommunityIn> GetOneAsync(int attachmentSetID, int tenantCommunityID, bool includeProperties = true);
        Task<int> AddAsync(VpnTenantCommunityIn vpnTenantCommunityIn);
        Task<int> UpdateAsync(VpnTenantCommunityIn vpnTenantCommunityIn);
        Task<int> DeleteAsync(VpnTenantCommunityIn vpnTenantCommunityIn);
    }
}
