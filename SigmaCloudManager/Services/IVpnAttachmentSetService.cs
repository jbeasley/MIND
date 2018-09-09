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

        Task<IEnumerable<VpnAttachmentSet>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<VpnAttachmentSet>> GetAllByVpnIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnAttachmentSet> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<int> AddAsync(VpnAttachmentSet attachmentSetVpn);
        Task<int> UpdateAsync(VpnAttachmentSet attachmentSetVpn);
        Task<int> DeleteAsync(VpnAttachmentSet attachmentSetVpn);
    }
}
