using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IRouteDistinguisherRangeService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<RouteDistinguisherRange>> GetAllAsync();
        Task<RouteDistinguisherRange> GetByIDAsync(int id);
        Task<RouteDistinguisherRange> GetByNameAsync(string name);
        Task<int> AddAsync(RouteDistinguisherRange routeDistinguisherRange);
        Task<int> UpdateAsync(RouteDistinguisherRange routeDistinguisherRange);
        Task<int> DeleteAsync(RouteDistinguisherRange routeDistinguisherRange);
    }
}
