using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Validators;

namespace Mind.Validators
{
    public interface IProviderDomainAttachmentValidator : IValidator
    {
        Task ValidateDeleteAsync(int attachmentId);
    }
}
