using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class MulticastVpnServiceTypeService : BaseService, IMulticastVpnServiceTypeService
    {
        public MulticastVpnServiceTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<MulticastVpnServiceType>> GetAllAsync()
        {
            return await this.UnitOfWork.MulticastVpnServiceTypeRepository.GetAsync(AsTrackable: false);
        }

        public async Task<MulticastVpnServiceType> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.MulticastVpnServiceTypeRepository.GetAsync(q => q.MulticastVpnServiceTypeID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(MulticastVpnServiceType multicastVpnServiceType)
        {
            this.UnitOfWork.MulticastVpnServiceTypeRepository.Insert(multicastVpnServiceType);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(MulticastVpnServiceType multicastVpnServiceType)
        {
            this.UnitOfWork.MulticastVpnServiceTypeRepository.Update(multicastVpnServiceType);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(MulticastVpnServiceType multicastVpnServiceType)
        {
            this.UnitOfWork.MulticastVpnServiceTypeRepository.Delete(multicastVpnServiceType);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
