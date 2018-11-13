using SCM.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;

namespace Mind.Builders
{
    public interface IBgpPeerUpdateDirector
    {
        Task<BgpPeer> UpdateAsync(int bgpPeerId, BgpPeerRequest request);
        Task<List<BgpPeer>> UpdateAsync(List<BgpPeerRequest> requests);
    }
}
