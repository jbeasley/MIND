using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Api.Models;
using SCM.Models;

namespace SCM.Api.Validators
{
    public interface IVpnApiValidator : IApiValidator
    {
        Task ValidateNewAsync(VpnRequestApiModel request);
        Task ValidateChangesAsync(Vpn vpn);
        void ValidateOkToSyncToNetwork(Vpn vpn);
    }
}
