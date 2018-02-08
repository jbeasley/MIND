using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IRouteTargetRangeService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<RouteTargetRange>> GetAllAsync();
        Task<RouteTargetRange> GetByIDAsync(int id);
        Task<RouteTargetRange> GetByNameAsync(string name);
        Task<int> AddAsync(RouteTargetRange routeTargetRange);
        Task<int> UpdateAsync(RouteTargetRange routeTargetRange);
        Task<int> DeleteAsync(RouteTargetRange routeTargetRange);
    }
}
