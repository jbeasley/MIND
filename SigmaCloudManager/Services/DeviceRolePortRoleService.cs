using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class DeviceRolePortRoleService : BaseService, IDeviceRolePortRoleService
    {
        public DeviceRolePortRoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "DeviceRole,"
            + "PortRole";


        public async Task<IEnumerable<DeviceRolePortRole>> GetAllByDeviceRoleIDAsync(int deviceRoleID, string searchString = "", bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.DeviceRolePortRoleRepository.GetAsync(q => q.DeviceRoleID == deviceRoleID, 
                includeProperties: p, AsTrackable: false);
        }

        public async Task<DeviceRolePortRole> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.DeviceRolePortRoleRepository.GetAsync(q => q.DeviceRolePortRoleID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(DeviceRolePortRole deviceRolePortRole)
        {
            this.UnitOfWork.DeviceRolePortRoleRepository.Insert(deviceRolePortRole);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(DeviceRolePortRole deviceRolePortRole)
        {
            this.UnitOfWork.DeviceRolePortRoleRepository.Update(deviceRolePortRole);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(DeviceRolePortRole deviceRolePortRole)
        {
            this.UnitOfWork.DeviceRolePortRoleRepository.Delete(deviceRolePortRole);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
