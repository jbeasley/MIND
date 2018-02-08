using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class RouteTargetRangeService : BaseService, IRouteTargetRangeService
    {
        public RouteTargetRangeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<RouteTargetRange>> GetAllAsync()
        {
            return await this.UnitOfWork.RouteTargetRangeRepository.GetAsync(AsTrackable: false);
        }

        public async Task<RouteTargetRange> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.RouteTargetRangeRepository.GetAsync(q => q.RouteTargetRangeID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<RouteTargetRange> GetByNameAsync(string name)
        {
            var dbResult = await this.UnitOfWork.RouteTargetRangeRepository.GetAsync(q => q.Name == name);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(RouteTargetRange routeTargetRange)
        {
            this.UnitOfWork.RouteTargetRangeRepository.Insert(routeTargetRange);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(RouteTargetRange routeTargetRange)
        {
            this.UnitOfWork.RouteTargetRangeRepository.Update(routeTargetRange);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(RouteTargetRange routeTargetRange)
        {
            this.UnitOfWork.RouteTargetRangeRepository.Delete(routeTargetRange);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
