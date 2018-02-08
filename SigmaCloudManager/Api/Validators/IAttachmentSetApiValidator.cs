using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Api.Models;
using SCM.Models;

namespace SCM.Api.Validators
{
    public interface IAttachmentSetApiValidator : IApiValidator
    {
        Task ValidateAsync(AttachmentSet attachmentSet);
        Task ValidateNewAsync(AttachmentSetRequestApiModel request);
        Task ValidateChangesAsync(AttachmentSet attachmentSet);
        Task ValidateDeleteAsync(AttachmentSet attachmentSet);
    }
}
