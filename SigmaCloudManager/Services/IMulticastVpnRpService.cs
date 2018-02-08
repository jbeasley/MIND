using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IMulticastVpnRpService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<MulticastVpnRp>> GetAllByVpnIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<MulticastVpnRp>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true);
        Task<MulticastVpnRp> GetByIDAsync(int id, bool includeProperties = true);
        Task<MulticastVpnRp> GetByIpAddressAsync(string ipAddress, bool includeProperties = true);
        Task<int> AddAsync(MulticastVpnRp multicastVpnRp);
        Task<int> UpdateAsync(MulticastVpnRp multicastVpnRp);
        Task<int> DeleteAsync(MulticastVpnRp multicastVpnRp);
    }
}
        
