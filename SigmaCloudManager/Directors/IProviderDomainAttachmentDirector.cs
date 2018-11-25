using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Directors;

namespace Mind.Builders
{
    public interface IProviderDomainAttachmentDirector
    {
        Task<Attachment> BuildAsync(int tenantId, ProviderDomainAttachmentRequest request, bool syncToNetwork = true);
        Task<Attachment> UpdateAsync(Attachment attachment, ProviderDomainAttachmentUpdate request, bool syncToNetwork = true);
        Task DestroyAsync(Attachment attachment, bool cleanUpNetwork = false);
        Task DestroyAsync(List<Attachment> attachments, bool cleanUpNetwork = false);
    }
}
