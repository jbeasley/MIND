using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IBgpPeerService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<BgpPeer>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<BgpPeer>> GetAllByRoutingInstanceIDAsync(int id, bool includeProperties = true);
        Task<BgpPeer> GetByIDAsync(int id, bool includeProperties = true);
        Task<int> AddAsync(BgpPeer bgpPeer);
        Task<int> UpdateAsync(BgpPeer bgpPeer);
        Task<int> DeleteAsync(BgpPeer bgpPeer);
    }
}
