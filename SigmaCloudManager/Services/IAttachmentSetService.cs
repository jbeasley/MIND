using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Models;
using SCM.Services;

namespace Mind.Services
{
    public interface IAttachmentSetService : IBaseService
    {
        Task<AttachmentSet> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<AttachmentSet>> GetAllByTenantIDAsync(int tenantId, bool? deep = false, bool asTrackable = false);
        Task<AttachmentSet> AddAsync(int tenantId, AttachmentSetRequest request);
        Task<AttachmentSet> AddAsync(AttachmentSet attachmentSet);
        Task<AttachmentSet> UpdateAsync(AttachmentSet attachmentSet);
        Task<AttachmentSet> UpdateAsync(int attachmentSet, AttachmentSetUpdate update);
        Task DeleteAsync(int attachmentSetId);
    }
}
