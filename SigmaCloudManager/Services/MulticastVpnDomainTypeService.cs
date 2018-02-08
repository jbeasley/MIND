using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class MulticastVpnDomainTypeService : BaseService, IMulticastVpnDomainTypeService
    {
        public MulticastVpnDomainTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<MulticastVpnDomainType>> GetAllAsync()
        {
            return await this.UnitOfWork.MulticastVpnDomainTypeRepository.GetAsync(AsTrackable: false);
        }

        public async Task<MulticastVpnDomainType> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.MulticastVpnDomainTypeRepository.GetAsync(q => q.MulticastVpnDomainTypeID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(MulticastVpnDomainType multicastVpnDomainType)
        {
            this.UnitOfWork.MulticastVpnDomainTypeRepository.Insert(multicastVpnDomainType);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(MulticastVpnDomainType multicastVpnDomainType)
        {
            this.UnitOfWork.MulticastVpnDomainTypeRepository.Update(multicastVpnDomainType);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(MulticastVpnDomainType multicastVpnDomainType)
        {
            this.UnitOfWork.MulticastVpnDomainTypeRepository.Delete(multicastVpnDomainType);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
