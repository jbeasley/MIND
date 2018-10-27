using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using SCM.Models.RequestModels;
using SCM.Services;
using Mind.Models.RequestModels;

namespace Mind.Services
{
    public interface IAttachmentSetRoutingInstanceService : IBaseService
    {
        Task<AttachmentSetRoutingInstance> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<AttachmentSetRoutingInstance>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<AttachmentSetRoutingInstance> GetByAttachmentSetIDAndRoutingInstanceIDAsync(int attachmentSetId, int routingInstanceId, bool? deep = false, bool asTrackable = false);
        Task<AttachmentSetRoutingInstance> AddAsync(int attachmentSetID, RoutingInstanceForAttachmentSetRequest request);
        Task<AttachmentSetRoutingInstance> UpdateAsync(AttachmentSetRoutingInstance attachmentSetRoutingInstance);
        Task DeleteAsync(int attachmentSetId, int routingInstanceId);
    }
}
