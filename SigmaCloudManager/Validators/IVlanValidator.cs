using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;

namespace SCM.Validators
{
    public interface IVlanValidator : IValidator
    {
        Task ValidateChangesAsync(Vlan vlan);
    }
}
