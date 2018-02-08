using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class DeviceRoleService : BaseService, IDeviceRoleService
    {
        public DeviceRoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<DeviceRole>> GetAllAsync(bool? isProviderDomainRole = null, bool? isTenantDomainRole = null)
        {
            var query = from deviceRoles in await this.UnitOfWork.DeviceRoleRepository.GetAsync(AsTrackable: false)
                        select deviceRoles;

            if (isProviderDomainRole != null)
            {
                query = query.Where(x => x.IsProviderDomainRole == isProviderDomainRole.Value);
            }
            
            if (isTenantDomainRole != null)
            {
                query = query.Where(x => x.IsTenantDomainRole == isTenantDomainRole.Value);
            }

            return query.ToList();
        }

        public async Task<DeviceRole> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.DeviceRoleRepository.GetAsync(q => q.DeviceRoleID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(DeviceRole deviceRole)
        {
            this.UnitOfWork.DeviceRoleRepository.Insert(deviceRole);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(DeviceRole deviceRole)
        {
            this.UnitOfWork.DeviceRoleRepository.Update(deviceRole);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(DeviceRole deviceRole)
        {
            this.UnitOfWork.DeviceRoleRepository.Delete(deviceRole);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
