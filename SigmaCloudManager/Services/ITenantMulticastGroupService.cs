using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface ITenantMulticastGroupService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<TenantMulticastGroup>> GetAllAsync(string searchString = "", bool includeProperties = true);
        Task<IEnumerable<TenantMulticastGroup>> GetAllByTenantIDAsync(int id, string searchString = "", bool includeProperties = true);
        Task<TenantMulticastGroup> GetByIDAsync(int id, bool includeProperties = true);
        Task<TenantMulticastGroup> GetByGroupAddressAsync(string groupAddress, bool includeProperties = true);
        Task<int> AddAsync(TenantMulticastGroup tenantMulticastGroup);
        Task<int> UpdateAsync(TenantMulticastGroup tenantMulticastGroup);
        Task<int> DeleteAsync(TenantMulticastGroup tenantMulticastGroup);
    }
}
        
