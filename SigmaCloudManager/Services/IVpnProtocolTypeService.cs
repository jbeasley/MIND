using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnProtocolTypeService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<VpnProtocolType>> GetAllAsync();
        Task<VpnProtocolType> GetByIDAsync(int id);
        Task<VpnProtocolType> GetByVpnTopologyTypeIDAsync(int id);
        Task<VpnProtocolType> GetByNameAsync(string name);
        Task<int> AddAsync(VpnProtocolType vpnProtocolType);
        Task<int> UpdateAsync(VpnProtocolType vpnProtocolType);
        Task<int> DeleteAsync(VpnProtocolType vpnProtocolType);
    }
}
