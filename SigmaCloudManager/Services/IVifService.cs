using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Data;
using System.Linq.Expressions;

namespace SCM.Services
{
    public interface IVifService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<Vif> GetByIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<Vif>> GetAllByRoutingInstanceIDAsync(int vrfID, bool includeProperties = true);
        Task<List<Vif>> GetAllByAttachmentIDAsync(int id, bool? roleRequireSyncToNetwork = null, bool? requiresSync = null,
            bool? created = null, bool? showRequiresSyncAlert = null, bool? showCreatedAlert = null, bool includeProperties = true);
        Task<List<Vif>> GetAllByVpnIDAsync(int vpnID, bool includeProperties = true);
        Task<ServiceResult> AddAsync(VifRequest request);
        Task<ServiceResult> UpdateAsync(VifUpdate vifUpdate);
        Task<int> UpdateAsync(IEnumerable<Vif> vifs);
        Task<ServiceResult> DeleteAsync(Vif vif);
    }
}