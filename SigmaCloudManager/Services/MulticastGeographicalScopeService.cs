using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class MulticastGeographicalScopeService : BaseService, IMulticastGeographicalScopeService
    {
        public MulticastGeographicalScopeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "VpnTenantMulticastGroups";

        public async Task<IEnumerable<MulticastGeographicalScope>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.MulticastGeographicalScopeRepository.GetAsync(includeProperties: p, AsTrackable: false);
        }

        public async Task<MulticastGeographicalScope> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.MulticastGeographicalScopeRepository.GetAsync(q => q.MulticastGeographicalScopeID == id,
                includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(MulticastGeographicalScope multicastGeographicalScope)
        {
            this.UnitOfWork.MulticastGeographicalScopeRepository.Insert(multicastGeographicalScope);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(MulticastGeographicalScope multicastGeographicalScope)
        {
            this.UnitOfWork.MulticastGeographicalScopeRepository.Update(multicastGeographicalScope);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(MulticastGeographicalScope multicastGeographicalScope)
        {
            this.UnitOfWork.MulticastGeographicalScopeRepository.Delete(multicastGeographicalScope);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
