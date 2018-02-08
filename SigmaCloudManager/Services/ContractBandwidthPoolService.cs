using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace SCM.Services
{
    public class ContractBandwidthPoolService : BaseService, IContractBandwidthPoolService
    {
        public ContractBandwidthPoolService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<ContractBandwidthPool>> GetAllAsync()
        {
            return await this.UnitOfWork.ContractBandwidthPoolRepository.GetAsync();
        }

        public async Task<ContractBandwidthPool> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.ContractBandwidthPoolRepository.GetAsync(q => q.ContractBandwidthPoolID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }
        public async Task<ContractBandwidthPool> GetByNameAsync(string name)
        {
            var dbResult = await this.UnitOfWork.ContractBandwidthPoolRepository.GetAsync(q => q.Name == name);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> DeleteAsync(ContractBandwidthPool contractBandwidthPool)
        {
            this.UnitOfWork.ContractBandwidthPoolRepository.Delete(contractBandwidthPool);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
