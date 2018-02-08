using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IRoutingPolicyMatchOptionService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<RoutingPolicyMatchOption>> GetAllAsync();
        Task<RoutingPolicyMatchOption> GetByIDAsync(int id);
        Task<int> AddAsync(RoutingPolicyMatchOption routingPolicyMatchOption);
        Task<int> UpdateAsync(RoutingPolicyMatchOption routingPolicyMatchOption);
        Task<int> DeleteAsync(RoutingPolicyMatchOption routingPolicyMatchOption);
    }
}
