using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface IInfrastructureAttachmentDirector
    {
        Task<Attachment> BuildAsync(int deviceId, InfrastructureAttachmentRequest request);
    }
}
