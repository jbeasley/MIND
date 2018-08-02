using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public interface IAttachmentSetValidator : IValidator
    {
        Task ValidateNewAsync(AttachmentSet attachmentSet);
        Task ValidateChangesAsync(AttachmentSet attachmentSet);
        Task ValidateDeleteAsync(AttachmentSet attachmentSet);
    }
}
