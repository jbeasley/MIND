using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVpnTenancyTypeService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<VpnTenancyType>> GetAllAsync();
        Task<VpnTenancyType> GetByIDAsync(int id);
        Task<VpnTenancyType> GetByNameAsync(string name);
        Task<int> AddAsync(VpnTenancyType vpnTenancyType);
        Task<int> UpdateAsync(VpnTenancyType vpnTenancyType);
        Task<int> DeleteAsync(VpnTenancyType vpnTenancyType);
    }
}
