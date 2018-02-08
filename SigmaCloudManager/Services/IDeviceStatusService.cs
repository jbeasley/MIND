using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IDeviceStatusService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<DeviceStatus>> GetAllAsync();
        Task<DeviceStatus> GetByIDAsync(int id);
        //Task<DeviceStatus> GetByNameAsync(string name);
        Task<int> AddAsync(DeviceStatus deviceStatus);
        Task<int> UpdateAsync(DeviceStatus deviceStatus);
        Task<int> DeleteAsync(DeviceStatus deviceStatus);
    }
}
