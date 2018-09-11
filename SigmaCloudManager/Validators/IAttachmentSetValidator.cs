using Mind.Models.RequestModels;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public interface IAttachmentSetValidator : IValidator
    {
        Task ValidateDeleteAsync(int attachmentSetId);
    }
}
