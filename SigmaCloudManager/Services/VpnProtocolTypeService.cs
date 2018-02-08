using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class VpnProtocolTypeService : BaseService, IVpnProtocolTypeService
    {
        public VpnProtocolTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<VpnProtocolType>> GetAllAsync()
        {
            return await this.UnitOfWork.VpnProtocolTypeRepository.GetAsync(AsTrackable: false);
        }

        public async Task<VpnProtocolType> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.VpnProtocolTypeRepository.GetAsync(q => q.VpnProtocolTypeID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<VpnProtocolType> GetByNameAsync(string name)
        {
            var dbResult = await this.UnitOfWork.VpnProtocolTypeRepository.GetAsync(q => q.Name == name,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<VpnProtocolType> GetByVpnTopologyTypeIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.VpnProtocolTypeRepository.GetAsync(q => q.VpnTopologyTypes
                .Where(x => x.VpnTopologyTypeID == id).Any(),
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(VpnProtocolType vpnProtocolType)
        {
            this.UnitOfWork.VpnProtocolTypeRepository.Insert(vpnProtocolType);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(VpnProtocolType vpnProtocolType)
        {
            this.UnitOfWork.VpnProtocolTypeRepository.Update(vpnProtocolType);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(VpnProtocolType vpnProtocolType)
        {
            this.UnitOfWork.VpnProtocolTypeRepository.Delete(vpnProtocolType);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
