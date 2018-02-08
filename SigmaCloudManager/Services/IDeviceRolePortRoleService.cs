using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IDeviceRolePortRoleService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<DeviceRolePortRole>> GetAllByDeviceRoleIDAsync(int deviceRoleID, string searchString = "", bool includeProperties = true);
        Task<DeviceRolePortRole> GetByIDAsync(int id);
        Task<int> AddAsync(DeviceRolePortRole deviceRolePortRole);
        Task<int> UpdateAsync(DeviceRolePortRole deviceRolePortRole);
        Task<int> DeleteAsync(DeviceRolePortRole deviceRolePortRole);
    }
}
