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
        Task ValidateDeleteAsync(int attachmentSetRoutingInstanceId);
        Task ValidateRoutingInstancesForVpnAsync(int vpnId);
        Task ValidateRoutingInstancesForAttachmentSetAsync(int IdattachmentSet);
    }
}
