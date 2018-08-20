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
        Task<IEnumerable<Tenant>> GetAllAsync(bool? deep = false, bool asTrackable = false);
        Task<Tenant> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<Tenant> GetByNameAsync(string name, bool? deep = false, bool asTrackable = false);
        Task<Tenant> AddAsync(Tenant tenant);
        Task<Tenant> UpdateAsync(Tenant tenant);
        Task DeleteAsync(int tenantId);
    }
}
