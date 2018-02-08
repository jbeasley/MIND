using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class MulticastVpnDirectionTypeService : BaseService, IMulticastVpnDirectionTypeService
    {
        public MulticastVpnDirectionTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<MulticastVpnDirectionType>> GetAllAsync()
        {
            return await this.UnitOfWork.MulticastVpnDirectionTypeRepository.GetAsync(AsTrackable: false);
        }

        public async Task<MulticastVpnDirectionType> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.MulticastVpnDirectionTypeRepository.GetAsync(q => q.MulticastVpnDirectionTypeID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(MulticastVpnDirectionType multicastVpnDirectionType)
        {
            this.UnitOfWork.MulticastVpnDirectionTypeRepository.Insert(multicastVpnDirectionType);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(MulticastVpnDirectionType multicastVpnDirectionType)
        {
            this.UnitOfWork.MulticastVpnDirectionTypeRepository.Update(multicastVpnDirectionType);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(MulticastVpnDirectionType multicastVpnDirectionType)
        {
            this.UnitOfWork.MulticastVpnDirectionTypeRepository.Delete(multicastVpnDirectionType);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
