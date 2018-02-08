using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface ILocationService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<Location>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<Location>> GetAllBySubRegionIDAsync(int subRegionID, bool includeProperties = true);
        Task<Location> GetByIDAsync(int id, bool includeProperties = true);
        Task<Location> GetByNameAsync(string siteName, bool includeProperties = true);
        Task<int> AddAsync(Location location);
        Task<int> UpdateAsync(Location location);
        Task<int> DeleteAsync(Location location);
    }
}
