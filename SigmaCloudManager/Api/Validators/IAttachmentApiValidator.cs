using SCM.Api.Models;
using SCM.Models;
using SCM.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Validators
{
    public interface IAttachmentApiValidator : IApiValidator
    {
        Task ValidateNewAsync(AttachmentRequestApiModel request);
        void ValidateDelete(Attachment attachment);
    }
}
