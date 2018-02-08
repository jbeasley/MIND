using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IContractBandwidthService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<ContractBandwidth>> GetAllAsync();
        Task<ContractBandwidth> GetByIDAsync(int id);
        Task<ContractBandwidth> GetAsync(int bandwidth);
        Task<int> AddAsync(ContractBandwidth contractBandwidth);
        Task<int> UpdateAsync(ContractBandwidth contractBandwidth);
        Task<int> DeleteAsync(ContractBandwidth contractBandwidth);
    }
}
