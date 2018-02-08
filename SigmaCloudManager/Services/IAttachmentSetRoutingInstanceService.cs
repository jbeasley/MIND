using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using SCM.Models.RequestModels;

namespace SCM.Services
{
    public interface IAttachmentSetRoutingInstanceService
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<AttachmentSetRoutingInstance>> GetAllAsync(bool includeProperties = true);
        Task<AttachmentSetRoutingInstance> GetByIDAsync(int id, bool includeProperties = true);
        Task<AttachmentSetRoutingInstance> GetByAttachmenSetAndRoutingInstanceAsync(int attachmentSetID, int vrfID, bool includeProperties = true);
        Task<IEnumerable<AttachmentSetRoutingInstance>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true);
        Task<ServiceResult> AddAsync(AttachmentSetRoutingInstance attachmentSetRoutingInstance);
        Task<ServiceResult> UpdateAsync(AttachmentSetRoutingInstance attachmentSetRoutingInstance);
        Task<ServiceResult> DeleteAsync(AttachmentSetRoutingInstance attachmentSetRoutingInstance);
        Task<IEnumerable<RoutingInstance>> GetCandidateRoutingInstances(AttachmentSetRoutingInstanceRequest request);
    }
}
