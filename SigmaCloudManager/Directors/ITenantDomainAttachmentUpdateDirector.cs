using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface ITenantDomainAttachmentUpdateDirector
    {
        Task<Attachment> UpdateAsync(Attachment attachment, TenantDomainAttachmentUpdate request);
    }
}
