using SCM.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Api.Validators
{
    public interface IVpnTenantNetworkInApiValidator : IApiValidator
    {
        Task ValidateNewAsync(VpnTenantNetworkInRequestApiModel request);
    }
}
