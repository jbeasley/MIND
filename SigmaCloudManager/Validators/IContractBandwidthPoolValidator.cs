using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public interface IContractBandwidthPoolValidator : IValidator
    {
        Task ValidateNewAsync(AttachmentRequest request);
        Task ValidateNewAsync(VifRequest request);
        Task ValidateChangesAsync(AttachmentUpdate attachmentUpdate);
        Task ValidateChangesAsync(VifUpdate update);
    }
}
