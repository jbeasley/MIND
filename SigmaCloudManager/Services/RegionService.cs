using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class RegionService : BaseService, IRegionService
    {
        public RegionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "SubRegions";

        public async Task<IEnumerable<Region>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.RegionRepository.GetAsync(AsTrackable: false, includeProperties: p);
        }

        public async Task<Region> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.RegionRepository.GetAsync(q => q.RegionID == id, 
                AsTrackable: false, includeProperties: p);
            return dbResult.SingleOrDefault();
        }

        public async Task<Region> GetByNameAsync(string name, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.RegionRepository.GetAsync(q => q.Name == name, includeProperties: p);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(Region region)
        {
            this.UnitOfWork.RegionRepository.Insert(region);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(Region region)
        {
            this.UnitOfWork.RegionRepository.Update(region);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(Region region)
        {
            this.UnitOfWork.RegionRepository.Delete(region);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
