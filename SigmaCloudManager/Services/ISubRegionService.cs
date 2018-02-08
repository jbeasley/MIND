using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface ISubRegionService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<SubRegion>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<SubRegion>> GetAllByRegionIDAsync(int regionID, bool includeProperties = true);
        Task<SubRegion> GetByIDAsync(int id, bool includeProperties = true);
        Task<SubRegion> GetByNameAsync(string name, bool includeProperties = true);
        Task<int> AddAsync(SubRegion subRegion);
        Task<int> UpdateAsync(SubRegion subRegion);
        Task<int> DeleteAsync(SubRegion subRegion);
    }
}
