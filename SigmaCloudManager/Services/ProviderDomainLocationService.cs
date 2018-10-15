using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class ProviderDomainLocationService : BaseService, IProviderDomainLocationService
    {
        public ProviderDomainLocationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<Location>> GetAllAsync(bool? deep = false, bool asTrackable = false, 
            string regionName = "", string subRegionName = "")
        {
            var query = (from result in await this.UnitOfWork.LocationRepository.GetAsync(
                      q => 
                        q.LocationType == LocationTypeEnum.ProviderDomain || q.LocationType == LocationTypeEnum.ProviderAndTenantDomain,
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                        AsTrackable: asTrackable)
                        select result);

            if (!string.IsNullOrEmpty(regionName)) query = query.Where(x => x.SubRegion.Region.Name == regionName);
            if (!string.IsNullOrEmpty(subRegionName)) query = query.Where(x => x.SubRegion.Name == subRegionName);

            return query.ToList();
        }

        public async Task<Location> GetByIDAsync(int locationId, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await this.UnitOfWork.LocationRepository.GetAsync(
                q =>
                    q.LocationID == locationId && 
                    q.LocationType == LocationTypeEnum.ProviderDomain || q.LocationType == LocationTypeEnum.ProviderAndTenantDomain,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }
    }
}
