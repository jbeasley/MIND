using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Validators;
using Mind.Models.RequestModels;

namespace Mind.Validators
{
    public interface IProviderDomainVifValidator : IValidator
    {
        Task ValidateDeleteAsync(int vifId);
    }
}
