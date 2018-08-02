using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public interface IBgpPeerValidator : IValidator
    {
        Task ValidateNewAsync(BgpPeer bgpPeer);
        void ValidateDelete(BgpPeer bgpPeer);
        Task ValidateChangesAsync(BgpPeer bgpPeer);
    }
}
