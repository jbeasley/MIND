using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public interface IMulticastVpnRpValidator : IValidator
    {
        Task ValidateNewAsync(MulticastVpnRp multicastVpnRp);
        Task ValidateDeleteAsync(MulticastVpnRp multicastVpnRp);
    }
}
