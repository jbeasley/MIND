using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using SCM.Services;

namespace Mind.Services
{
    public interface ITenantService : IBaseService
    {
        Task<IEnumerable<Tenant>> GetAllAsync();
        Task<Tenant> GetByIDAsync(int id);
        Task<Tenant> GetByNameAsync(string name);
        Task<int> AddAsync(Tenant tenant);
        Task<int> UpdateAsync(Tenant tenant);
        Task<int> DeleteAsync(int tenantId);
    }
}
