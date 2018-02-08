using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class SubRegionService : BaseService, ISubRegionService
    {
        public SubRegionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "Locations,Region";

        public async Task<IEnumerable<SubRegion>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.SubRegionRepository.GetAsync(AsTrackable: false, includeProperties: p);
        }

        public async Task<IEnumerable<SubRegion>> GetAllByRegionIDAsync(int regionID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.SubRegionRepository.GetAsync(q => q.RegionID == regionID, AsTrackable: false, includeProperties: p);
        }

        public async Task<SubRegion> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.SubRegionRepository.GetAsync(q => q.SubRegionID == id,
                AsTrackable: false, includeProperties: p);
            return dbResult.SingleOrDefault();
        }

        public async Task<SubRegion> GetByNameAsync(string name, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.SubRegionRepository.GetAsync(q => q.Name == name,
                AsTrackable: false, includeProperties: p);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(SubRegion subRegion)
        {
            this.UnitOfWork.SubRegionRepository.Insert(subRegion);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(SubRegion subRegion)
        {
            this.UnitOfWork.SubRegionRepository.Update(subRegion);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(SubRegion subRegion)
        {
            this.UnitOfWork.SubRegionRepository.Delete(subRegion);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
