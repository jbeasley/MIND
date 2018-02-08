using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class MtuService : BaseService, IMtuService
    {
        public MtuService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<Mtu>> GetAllAsync()
        {
            return await this.UnitOfWork.MtuRepository.GetAsync(AsTrackable: false);
        }

        public async Task<Mtu> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.MtuRepository.GetAsync(q => q.MtuID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<Mtu> GetByValueAsync(int mtuValue)
        {
            var dbResult = await this.UnitOfWork.MtuRepository.GetAsync(q => q.MtuValue == mtuValue);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(Mtu mtu)
        {
            this.UnitOfWork.MtuRepository.Insert(mtu);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(Mtu mtu)
        {
            this.UnitOfWork.MtuRepository.Update(mtu);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(Mtu mtu)
        {
            this.UnitOfWork.MtuRepository.Delete(mtu);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
