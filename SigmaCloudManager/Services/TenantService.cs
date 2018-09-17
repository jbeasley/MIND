using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using Microsoft.EntityFrameworkCore;
using SCM.Validators;
using SCM.Services;

namespace Mind.Services
{
    public class TenantService : BaseService, ITenantService
    {
        public TenantService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<Tenant>> GetAllAsync(bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.TenantRepository.GetAsync(
                query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                AsTrackable: asTrackable);
        }

        public async Task<Tenant> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await this.UnitOfWork.TenantRepository.GetAsync(
                q => 
                    q.TenantID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }
    
        public async Task<Tenant> GetByNameAsync(string name, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await this.UnitOfWork.TenantRepository.GetAsync(
                q => 
                    q.Name == name,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        public async Task<Tenant> AddAsync(Tenant tenant)
        {
            this.UnitOfWork.TenantRepository.Insert(tenant);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(tenant.TenantID, deep: true, asTrackable: false);
        }
 
        public async Task<Tenant> UpdateAsync(Tenant tenant)
        {
            this.UnitOfWork.TenantRepository.Update(tenant);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(tenant.TenantID, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int tenantId)
        {
            var tenant = (from result in await UnitOfWork.TenantRepository.GetAsync(
                        q =>
                          q.TenantID == tenantId,
                          query: q => q.IncludeDeleteValidationProperties(),
                          AsTrackable: true)
                          select result)
                          .Single();

            tenant.ValidateDelete();
            await this.UnitOfWork.TenantRepository.DeleteAsync(tenantId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}
