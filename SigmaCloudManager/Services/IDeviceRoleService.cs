using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IDeviceRoleService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<DeviceRole>> GetAllAsync(bool? isProviderDomainRole = null, bool? isTenantDomainRole = null);
        Task<DeviceRole> GetByIDAsync(int id);
        //Task<DeviceRole> GetByNameAsync(string name);
        Task<int> AddAsync(DeviceRole deviceRole);
        Task<int> UpdateAsync(DeviceRole deviceRole);
        Task<int> DeleteAsync(DeviceRole deviceRole);
    }
}
