using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IAttachmentSetRoutingInstanceDirector
    {
        Task<AttachmentSetRoutingInstance> BuildAsync(int attachmentSetId, RoutingInstanceForAttachmentSetRequest request);
        Task<AttachmentSetRoutingInstance> BuildAsync(AttachmentSet attachmentSet, RoutingInstanceForAttachmentSetRequest request);
        Task<List<SCM.Models.AttachmentSetRoutingInstance>> BuildAsync(AttachmentSet attachmentSet, List<RoutingInstanceForAttachmentSetRequest> requests);
    }
}
