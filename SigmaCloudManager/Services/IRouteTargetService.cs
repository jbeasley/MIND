using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using SCM.Models.RequestModels;

namespace SCM.Services
{
    public interface IRouteTargetService
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<RouteTarget>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<RouteTarget>> GetAllByVpnIDAsync(int id, bool includeProperties = true);
        Task<RouteTarget> GetByIDAsync(int id, bool includeProperties = true);
        Task<ServiceResult> AddAsync(RouteTargetRequest request);
        Task<int> DeleteAsync(RouteTarget routeTarget);
    }
}
