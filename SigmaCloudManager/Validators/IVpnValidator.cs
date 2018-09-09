using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace SCM.Validators
{
    public interface IVpnValidator : IValidator
    {
        Task ValidateDeleteAsync(int VpnId);
    }
}
