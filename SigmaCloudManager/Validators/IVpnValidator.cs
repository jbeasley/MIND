using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public interface IVpnValidator : IValidator
    {
        Task ValidateNewAsync(VpnRequest vpn);
        Task ValidateChangesAsync(Vpn vpn);
        void ValidateDelete(Vpn vpn);
        void ValidateOkToSyncToNetwork(Vpn vpn);
    }
}
