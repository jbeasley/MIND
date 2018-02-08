using SCM.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Validators
{ 
    public interface IAttachmentSetRoutingInstanceApiValidator : IApiValidator
    {
        Task ValidateNewAsync(AttachmentSetRoutingInstanceRequestApiModel request);
    }
}
