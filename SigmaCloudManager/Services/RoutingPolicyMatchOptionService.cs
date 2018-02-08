using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class RoutingPolicyMatchOptionService : BaseService, IRoutingPolicyMatchOptionService
    {
        public RoutingPolicyMatchOptionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<RoutingPolicyMatchOption>> GetAllAsync()
        {
            return await this.UnitOfWork.RoutingPolicyMatchOptionRepository.GetAsync(AsTrackable: false);
        }

        public async Task<RoutingPolicyMatchOption> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.RoutingPolicyMatchOptionRepository.GetAsync(q => q.RoutingPolicyMatchOptionID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(RoutingPolicyMatchOption routingPolicyMatchOption)
        {
            this.UnitOfWork.RoutingPolicyMatchOptionRepository.Insert(routingPolicyMatchOption);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(RoutingPolicyMatchOption routingPolicyMatchOption)
        {
            this.UnitOfWork.RoutingPolicyMatchOptionRepository.Update(routingPolicyMatchOption);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(RoutingPolicyMatchOption routingPolicyMatchOption)
        {
            this.UnitOfWork.RoutingPolicyMatchOptionRepository.Delete(routingPolicyMatchOption);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
