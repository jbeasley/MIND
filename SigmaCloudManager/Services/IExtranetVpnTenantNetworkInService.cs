using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IExtranetVpnTenantNetworkInService
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<ExtranetVpnTenantNetworkIn>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<ExtranetVpnTenantNetworkIn>> GetAllByTenantNetworkIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<ExtranetVpnTenantNetworkIn>> GetAllByExtranetVpnIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<ExtranetVpnTenantNetworkIn>> GetAllByExtranetVpnMemberIDAsync(int id, bool includeProperties = true);
        Task<ExtranetVpnTenantNetworkIn> GetByIDAsync(int id, bool includeProperties = true);
        Task<int> AddAsync(ExtranetVpnTenantNetworkIn extranetVpnTenantNetworkIn);
        Task<int> UpdateAsync(ExtranetVpnTenantNetworkIn extranetVpnTenantNetworkIn);
        Task<int> DeleteAsync(ExtranetVpnTenantNetworkIn extranetVpnTenantNetworkIn);
    }
}
