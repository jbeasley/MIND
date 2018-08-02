using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public interface IAttachmentSetRoutingInstanceValidator : IValidator
    {
        Task ValidateNewAsync(AttachmentSetRoutingInstance attachmentSetRoutingInstance);
        Task ValidateDeleteAsync(AttachmentSetRoutingInstance attachmentSetRoutingInstance);
        Task ValidateRoutingInstancesConfiguredCorrectlyAsync(Vpn vpn);
        Task ValidateRoutingInstancesConfiguredCorrectlyAsync(AttachmentSet attachmentSet);
    }
}
