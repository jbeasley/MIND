using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public interface IAttachmentValidator : IValidator
    {
        Task ValidateNewAsync(AttachmentRequest request);
        void ValidateDelete(Attachment attachment);
        Task ValidateChangesAsync(AttachmentUpdate update);
        Task ValidateChangesAsync(AttachmentPortUpdate update);
        Task ValidateChangesAsync(Interface update);
        Task ValidateRequiresNetworkSyncAsync(Vpn vpn);
        Task ValidateAttachmentPortsConfiguredCorrectlyAsync(Attachment attachment);
    }
}
