using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using Mind.Models.RequestModels;
using SCM.Services;

namespace Mind.Services
{
    public interface IProviderDomainBgpPeerService : IBaseService
    {
        Task<IEnumerable<BgpPeer>> GetAllByRoutingInstanceIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<BgpPeer> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<BgpPeer> AddAsync(int routingInstanceId, BgpPeerRequest request);
        Task<BgpPeer> UpdateAsync(int bgpPeerId, BgpPeerRequest request);
        Task DeleteAsync(int bgpPeerId);
    }
}
