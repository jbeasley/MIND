using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class PortRoleService : BaseService, IPortRoleService
    {
        public PortRoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<PortRole>> GetAllAsync()
        {
            return await this.UnitOfWork.PortRoleRepository.GetAsync(AsTrackable: false);
        }

        public async Task<IEnumerable<PortRole>> GetAllByDeviceRoleIDAsync(int deviceRoleID)
        {
            return await this.UnitOfWork.PortRoleRepository.GetAsync(q => q.DeviceRolePortRoles
                .Where(x => x.DeviceRoleID == deviceRoleID).Any(),
                AsTrackable: false);
        }

        public async Task<PortRole> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.PortRoleRepository.GetAsync(q => q.PortRoleID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<PortRole> GetByNameAsync(string name)
        {
            var dbResult = await this.UnitOfWork.PortRoleRepository.GetAsync(q => q.Name == name);
            return dbResult.SingleOrDefault();
        }

        public async Task<PortRole> GetByPortRoleTypeAsync(PortRoleType portRoleType)
        {
            var dbResult = await this.UnitOfWork.PortRoleRepository.GetAsync(q => q.PortRoleType == portRoleType);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(PortRole portRole)
        {
            this.UnitOfWork.PortRoleRepository.Insert(portRole);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(PortRole portRole)
        {
            this.UnitOfWork.PortRoleRepository.Update(portRole);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(PortRole portRole)
        {
            this.UnitOfWork.PortRoleRepository.Delete(portRole);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
