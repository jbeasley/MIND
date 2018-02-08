using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class DeviceStatusService : BaseService, IDeviceStatusService
    {
        public DeviceStatusService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<DeviceStatus>> GetAllAsync()
        {
            return await this.UnitOfWork.DeviceStatusRepository.GetAsync(AsTrackable: false);
        }

        public async Task<DeviceStatus> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.DeviceStatusRepository.GetAsync(q => q.DeviceStatusID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(DeviceStatus deviceStatus)
        {
            this.UnitOfWork.DeviceStatusRepository.Insert(deviceStatus);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(DeviceStatus deviceStatus)
        {
            this.UnitOfWork.DeviceStatusRepository.Update(deviceStatus);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(DeviceStatus deviceStatus)
        {
            this.UnitOfWork.DeviceStatusRepository.Delete(deviceStatus);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
