using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using SCM.Models.RequestModels;

namespace SCM.Services
{
    public interface IContractBandwidthPoolService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<ContractBandwidthPool>> GetAllAsync();
        Task<ContractBandwidthPool> GetByIDAsync(int id);
        Task<ContractBandwidthPool> GetByNameAsync(string name);
    }
}
