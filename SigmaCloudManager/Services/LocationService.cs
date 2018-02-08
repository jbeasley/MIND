using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class LocationService : BaseService, ILocationService
    {
        public LocationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "SubRegion.Region";

        public async Task<IEnumerable<Location>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.LocationRepository.GetAsync(AsTrackable: false, includeProperties: p);
        }

        public async Task<IEnumerable<Location>> GetAllBySubRegionIDAsync(int subRegionID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.LocationRepository.GetAsync(q => q.SubRegionID == subRegionID, AsTrackable: false, includeProperties: p);
        }

        public async Task<Location> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.LocationRepository.GetAsync(q => q.LocationID == id,
                AsTrackable: false, includeProperties: p);
            return dbResult.SingleOrDefault();
        }
 
        public async Task<Location> GetByNameAsync(string siteName, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult =  await this.UnitOfWork.LocationRepository.GetAsync(q => q.SiteName == siteName, includeProperties: p);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(Location location)
        {
            this.UnitOfWork.LocationRepository.Insert(location);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(Location location)
        {
            this.UnitOfWork.LocationRepository.Update(location);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(Location location)
        {
            this.UnitOfWork.LocationRepository.Delete(location);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
