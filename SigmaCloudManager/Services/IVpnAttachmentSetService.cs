using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnAttachmentSetService
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<VpnAttachmentSet>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<VpnAttachmentSet>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnAttachmentSet>> GetAllByVpnIDAsync(int id, bool includeProperties = true);
        Task<VpnAttachmentSet> GetByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true);
        Task<VpnAttachmentSet> GetByIDAsync(int id, bool includeProperties = true);
        Task<int> AddAsync(VpnAttachmentSet attachmentSetVpn);
        Task<int> UpdateAsync(VpnAttachmentSet attachmentSetVpn);
        Task<int> DeleteAsync(VpnAttachmentSet attachmentSetVpn);
    }
}
