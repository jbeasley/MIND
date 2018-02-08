using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnTopologyTypeService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<VpnTopologyType>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<VpnTopologyType>> GetByVpnProtocolTypeIDAsync(int id, bool includeProperties = true);
        Task<VpnTopologyType> GetByIDAsync(int id,bool includeProperties = true);
        Task<VpnTopologyType> GetByNameAsync(string name, bool includeProperties = true);
        Task<int> AddAsync(VpnTopologyType vpnTopologyType);
        Task<int> UpdateAsync(VpnTopologyType vpnTopologyType);
        Task<int> DeleteAsync(VpnTopologyType vpnTopologyType);
    }
}
