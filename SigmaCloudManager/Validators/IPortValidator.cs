using SCM.Api.Models;
using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public interface IPortValidator : IValidator
    {
        Task ValidateNewAsync(Port port);
        Task ValidateDeleteAsync(Port port);
        Task ValidateChangesAsync(Port port);
    }
}
