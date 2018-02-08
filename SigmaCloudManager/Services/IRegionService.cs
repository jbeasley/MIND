using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IRegionService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<Region>> GetAllAsync(bool includeProperties = true);
        Task<Region> GetByIDAsync(int id, bool includeProperties = true);
        Task<Region> GetByNameAsync(string name, bool includeProperties = true);
        Task<int> AddAsync(Region region);
        Task<int> UpdateAsync(Region region);
        Task<int> DeleteAsync(Region region);
    }
}
