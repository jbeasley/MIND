using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface ITenantDomainAttachmentDirector
    {
        Task<Attachment> BuildAsync(int tenantId, TenantDomainAttachmentRequest request);
        Task<Attachment> UpdateAsync(Attachment attachment, TenantDomainAttachmentUpdate request);
        Task DestroyAsync(Attachment attachment);
        Task DestroyAsync(List<Attachment> attachments);

    }
}
