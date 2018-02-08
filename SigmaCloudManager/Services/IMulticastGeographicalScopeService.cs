using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IMulticastGeographicalScopeService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<MulticastGeographicalScope>> GetAllAsync(bool includeProperties = true);
        Task<MulticastGeographicalScope> GetByIDAsync(int id, bool includeProperties = true);
        Task<int> AddAsync(MulticastGeographicalScope multicastGeographicalScope);
        Task<int> UpdateAsync(MulticastGeographicalScope multicastGeographicalScope);
        Task<int> DeleteAsync(MulticastGeographicalScope multicastGeographicalScope);
    }
}
        
