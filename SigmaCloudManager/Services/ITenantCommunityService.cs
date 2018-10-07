using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Services;
using Mind.Models.RequestModels;

namespace Mind.Services
{
    public interface ITenantCommunityService : IBaseService
    {
        Task<IEnumerable<TenantCommunity>> GetAllByTenantCommunitySetIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<TenantCommunity>> GetAllByTenantIDAsync(int id, string searchString = "", bool? deep = false, bool asTrackable = false);
        Task<TenantCommunity> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<TenantCommunity> GetByCommunityAsync(int asNumber, int number, bool? deep = false, bool asTrackable = false);
        Task<TenantCommunity> AddAsync(TenantCommunity tenantCommunity);
        Task<TenantCommunity> AddAsync(int tenantId, TenantCommunityRequest request);
        Task<TenantCommunity> UpdateAsync(TenantCommunity tenantCommunity);
        Task<TenantCommunity> UpdateAsync(int tenantCommunityId, TenantCommunityRequest update);
        Task DeleteAsync(int tenantCommunityId);
    }
}
