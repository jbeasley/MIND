using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public interface IVifValidator : IValidator
    {
        Task ValidateNewAsync(VifRequest request);
        void ValidateDelete(Vif vif);
        Task ValidateChangesAsync(VifUpdate vif);
        Task ValidateRequiresNetworkSyncAsync(Vpn vpn);
    }
}
