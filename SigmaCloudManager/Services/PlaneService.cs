using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class PlaneService : BaseService, IPlaneService
    {
        public PlaneService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<Plane>> GetAllAsync()
        {
            return await this.UnitOfWork.PlaneRepository.GetAsync(AsTrackable: false);
        }

        public async Task<Plane> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.PlaneRepository.GetAsync(q => q.PlaneID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<Plane> GetByNameAsync(string planeName)
        {
            var dbResult = await this.UnitOfWork.PlaneRepository.GetAsync(q => q.Name == planeName,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(Plane plane)
        {
            this.UnitOfWork.PlaneRepository.Insert(plane);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(Plane plane)
        {
            this.UnitOfWork.PlaneRepository.Update(plane);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(Plane plane)
        {
            this.UnitOfWork.PlaneRepository.Delete(plane);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
