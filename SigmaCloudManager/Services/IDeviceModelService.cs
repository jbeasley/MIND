using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IDeviceModelService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<DeviceModel>> GetAllAsync();
        Task<DeviceModel> GetByIDAsync(int id);
        //Task<DeviceModel> GetByNameAsync(string name);
        Task<int> AddAsync(DeviceModel deviceModel);
        Task<int> UpdateAsync(DeviceModel deviceModel);
        Task<int> DeleteAsync(DeviceModel deviceModel);
    }
}
