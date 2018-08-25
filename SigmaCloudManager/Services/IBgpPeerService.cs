using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IBgpPeerService : IBaseService
    {
        Task<IEnumerable<BgpPeer>> GetAllByRoutingInstanceIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<BgpPeer> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<BgpPeer> AddAsync(BgpPeer bgpPeer);
        Task<BgpPeer> UpdateAsync(BgpPeer bgpPeer);
        Task DeleteAsync(int bgpPeerId);
    }
}
