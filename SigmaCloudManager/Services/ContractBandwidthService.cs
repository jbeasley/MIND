using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class ContractBandwidthService : BaseService, IContractBandwidthService
    {
        public ContractBandwidthService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<ContractBandwidth>> GetAllAsync()
        {
            return await this.UnitOfWork.ContractBandwidthRepository.GetAsync();
        }
   
        public async Task<ContractBandwidth> GetAsync(int bandwidth)
        {
            var dbResult = await this.UnitOfWork.ContractBandwidthRepository.GetAsync(q => q.BandwidthMbps == bandwidth);
            return dbResult.SingleOrDefault();
        }

        public async Task<ContractBandwidth> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.ContractBandwidthRepository.GetAsync(q => q.ContractBandwidthID == id,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(ContractBandwidth contractBandwidth)
        {
            this.UnitOfWork.ContractBandwidthRepository.Insert(contractBandwidth);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(ContractBandwidth contractBandwidth)
        {
            this.UnitOfWork.ContractBandwidthRepository.Update(contractBandwidth);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(ContractBandwidth contractBandwidth)
        {
            this.UnitOfWork.ContractBandwidthRepository.Delete(contractBandwidth);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
