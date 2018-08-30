using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class RouteDistinguisherRangeService : BaseService, IRouteDistinguisherRangeService
    {
        public RouteDistinguisherRangeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<RouteDistinguisherRange>> GetAllAsync()
        {
            return await this.UnitOfWork.RouteDistinguisherRangeRepository.GetAsync(AsTrackable: false);
        }

        public async Task<RouteDistinguisherRange> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.RouteDistinguisherRangeRepository.GetAsync(q => q.RouteDistinguisherRangeID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<RouteDistinguisherRange> GetByTypeAsync(RouteDistinguisherRangeTypeEnum rdRangeType)
        {
            var dbResult = await this.UnitOfWork.RouteDistinguisherRangeRepository.GetAsync(q => q.Type == rdRangeType);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(RouteDistinguisherRange routeDistinguisherRange)
        {
            this.UnitOfWork.RouteDistinguisherRangeRepository.Insert(routeDistinguisherRange);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(RouteDistinguisherRange routeDistinguisherRange)
        {
            this.UnitOfWork.RouteDistinguisherRangeRepository.Update(routeDistinguisherRange);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(RouteDistinguisherRange routeDistinguisherRange)
        {
            this.UnitOfWork.RouteDistinguisherRangeRepository.Delete(routeDistinguisherRange);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
