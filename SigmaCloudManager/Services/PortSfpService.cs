using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class PortSfpService : BaseService, IPortSfpService
    {
        public PortSfpService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<PortSfp>> GetAllAsync()
        {
            return await this.UnitOfWork.PortSfpRepository.GetAsync(AsTrackable: false);
        }

        public async Task<PortSfp> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.PortSfpRepository.GetAsync(q => q.PortSfpID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(PortSfp portSfp)
        {
            this.UnitOfWork.PortSfpRepository.Insert(portSfp);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(PortSfp portSfp)
        {
            this.UnitOfWork.PortSfpRepository.Update(portSfp);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(PortSfp portSfp)
        {
            this.UnitOfWork.PortSfpRepository.Delete(portSfp);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
