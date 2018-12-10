using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using Mind.Models.RequestModels;
using SCM.Services;

namespace Mind.Services
{
    public interface IVpnService : IBaseService
    {
        Task<IEnumerable<Vpn>> GetAllAsync(bool? isExtranet = null, bool? created = null, bool? showCreatedAlert = null, 
           bool? deep = false, bool asTrackable = false, string sortKey = "", string searchString = "");
        Task<IEnumerable<Vpn>> GetAllByAttachmentSetIDAsync(int id, bool? isExtranet = null, bool? created = null, bool? showCreatedAlert = null,
           bool? deep = false, bool asTrackable = false, string sortKey = "", string searchString = "");
        Task<IEnumerable<Vpn>> GetAllByTenantIDAsync(int id, bool? isExtranet = null, bool? created = null, bool? showCreatedAlert = null,
           bool? deep = false, bool asTrackable = false, string sortKey = "", string searchString = "");
        Task<Vpn> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<Vpn> AddAsync(int tenantId, VpnRequest vpnRequest, bool syncToNetwork = false);
        Task<Vpn> UpdateAsync(int vpnId, VpnUpdate update, bool syncToNetworkPut = false, bool syncToNetworkPatch = false);
        Task DeleteAsync(int vpnId);
        Task SyncToNetworkPutAsync(int vpnId);
    }
}
