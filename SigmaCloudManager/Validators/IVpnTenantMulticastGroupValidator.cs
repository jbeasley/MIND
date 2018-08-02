using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public interface IVpnTenantMulticastGroupValidator : IValidator
    {
        Task ValidateNewAsync(VpnTenantMulticastGroup vpnTenantMulticastGroup);
        Task ValidateChangesAsync(VpnTenantMulticastGroup vpnTenantMulticastGroup);
    }
}
