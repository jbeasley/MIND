using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class VpnTenancyTypeService : BaseService, IVpnTenancyTypeService
    {
        public VpnTenancyTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<VpnTenancyType>> GetAllAsync()
        {
            return await this.UnitOfWork.VpnTenancyTypeRepository.GetAsync(AsTrackable: false);
        }

        public async Task<VpnTenancyType> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.VpnTenancyTypeRepository.GetAsync(q => q.VpnTenancyTypeID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<VpnTenancyType> GetByNameAsync(string name)
        {
            var dbResult = await this.UnitOfWork.VpnTenancyTypeRepository.GetAsync(q => q.Name == name,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(VpnTenancyType vpnTenancyType)
        {
            this.UnitOfWork.VpnTenancyTypeRepository.Insert(vpnTenancyType);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(VpnTenancyType vpnTenancyType)
        {
            this.UnitOfWork.VpnTenancyTypeRepository.Update(vpnTenancyType);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(VpnTenancyType vpnTenancyType)
        {
            this.UnitOfWork.VpnTenancyTypeRepository.Delete(vpnTenancyType);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
