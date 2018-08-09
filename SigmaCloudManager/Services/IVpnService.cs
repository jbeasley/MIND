using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using SCM.Models.RequestModels;

namespace SCM.Services
{
    public interface IVpnService
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<Vpn>> GetAllAsync(bool? isExtranet = null, 
            bool? requiresSync = null, bool? created = null, bool? showRequiresSyncAlert = null, bool? showCreatedAlert = null, 
            string searchString = "", bool includeProperties = true, string sortKey = "");
        Task<IEnumerable<Vpn>> GetAllByRoutingInstanceIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<Vpn>> GetAllByAttachmentSetIDAsync(int id, bool? requiresSync = null, bool? created = null,
                   bool? showRequiresSyncAlert = null, bool? showCreatedAlert = null, bool includeProperties = true);
        Task<IEnumerable<Vpn>> GetAllByTenantIDAsync(int id, bool? isExtranet = null, bool includeProperties = true);
        Task<IEnumerable<Vpn>> GetAllByTenantNetworkIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<Vpn>> GetAllByTenantCommunityIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<Vpn>> GetAllByTenantCommunitySetIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<Vpn>> GetAllByTenantMulticastGroupIDAsync(int id, bool includeProperties = true);
        Task<Vpn> GetByIDAsync(int id, bool includeProperties = true);
        Task<ServiceResult> AddAsync(VpnRequest vpnRequest);
        Task<ServiceResult> UpdateAsync(Vpn vpn);
        Task<int> UpdateAsync(IEnumerable<Vpn> vpns);
        Task<ServiceResult> DeleteAsync(Vpn vpn);
    }
}
