using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnTenantMulticastGroupService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<VpnTenantMulticastGroup>> GetAllByVpnIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantMulticastGroup>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantMulticastGroup>> GetAllByMulticastVpnRpIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<VpnTenantMulticastGroup>> GetAllByTenantMulticastGroupIDAsync(int id, bool includeProperties = true);
        Task<VpnTenantMulticastGroup> GetByIDAsync(int id, bool includeProperties = true);
        Task<VpnTenantMulticastGroup> GetByGroupAddressAsync(string groupAddress, bool includeProperties = true);
        Task<int> AddAsync(VpnTenantMulticastGroup vpnTenantMulticastGroup);
        Task<int> UpdateAsync(VpnTenantMulticastGroup vpnTenantMulticastGroup);
        Task<int> DeleteAsync(VpnTenantMulticastGroup vpnTenantMulticastGroup);
    }
}
        
