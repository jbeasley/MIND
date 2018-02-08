using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class DeviceModelService : BaseService, IDeviceModelService
    {
        public DeviceModelService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<DeviceModel>> GetAllAsync()
        {
            return await this.UnitOfWork.DeviceModelRepository.GetAsync(AsTrackable: false);
        }

        public async Task<DeviceModel> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.DeviceModelRepository.GetAsync(q => q.DeviceModelID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(DeviceModel deviceModel)
        {
            this.UnitOfWork.DeviceModelRepository.Insert(deviceModel);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(DeviceModel deviceModel)
        {
            this.UnitOfWork.DeviceModelRepository.Update(deviceModel);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(DeviceModel deviceModel)
        {
            this.UnitOfWork.DeviceModelRepository.Delete(deviceModel);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
