using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using Mind.Models.RequestModels;
using SCM.Services;

namespace Mind.Services
{
    public interface IVpnAttachmentSetService : IBaseService
    {
        Task<IEnumerable<VpnAttachmentSet>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<VpnAttachmentSet>> GetAllByVpnIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnAttachmentSet> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnAttachmentSet> GetByVpnIDAndAttachmentSetIDAsync(int vpnId, int attachmentSetId, bool? deep = false, bool asTrackable = false);
        Task<VpnAttachmentSet> AddAsync(int vpnId, VpnAttachmentSetRequest request);
        Task<VpnAttachmentSet> UpdateAsync(int vpnId, int attachmentSetId, VpnAttachmentSetUpdate update);
        Task DeleteAsync(int vpnAttachmentSetId);
        Task DeleteAsync(int vpnId, int attachmentSetId);
    }
}
