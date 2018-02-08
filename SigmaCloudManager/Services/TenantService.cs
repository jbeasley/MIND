using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using Microsoft.EntityFrameworkCore;

namespace SCM.Services
{
    public class TenantService : BaseService, ITenantService
    {
        public TenantService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<Tenant>> GetAllAsync()
        {
            return await this.UnitOfWork.TenantRepository.GetAsync(AsTrackable: false);
        }

        public async Task<Tenant> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.TenantRepository.GetAsync(q => q.TenantID == id, AsTrackable: false);
            return dbResult.SingleOrDefault();
        }
    
        public async Task<Tenant> GetByNameAsync(string name)
        {
            var dbResult = await this.UnitOfWork.TenantRepository.GetAsync(q => q.Name == name);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(Tenant tenant)
        {
            this.UnitOfWork.TenantRepository.Insert(tenant);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(Tenant tenant)
        {
            this.UnitOfWork.TenantRepository.Update(tenant);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(Tenant tenant)
        {
            this.UnitOfWork.TenantRepository.Delete(tenant);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
