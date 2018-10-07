using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface IInfrastructureAttachmentUpdateDirector
    {
        Task<Attachment> UpdateAsync(Attachment attachment, InfrastructureAttachmentUpdate request);
    }
}
