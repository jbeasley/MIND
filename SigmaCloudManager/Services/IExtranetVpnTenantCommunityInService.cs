using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IExtranetVpnTenantCommunityInService
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<ExtranetVpnTenantCommunityIn>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<ExtranetVpnTenantCommunityIn>> GetAllByTenantCommunityIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<ExtranetVpnTenantCommunityIn>> GetAllByExtranetVpnIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<ExtranetVpnTenantCommunityIn>> GetAllByExtranetVpnMemberIDAsync(int id, bool includeProperties = true);
        Task<ExtranetVpnTenantCommunityIn> GetByIDAsync(int id, bool includeProperties = true);
        Task<int> AddAsync(ExtranetVpnTenantCommunityIn extranetVpnTenantCommunityIn);
        Task<int> UpdateAsync(ExtranetVpnTenantCommunityIn extranetVpnTenantCommunityIn);
        Task<int> DeleteAsync(ExtranetVpnTenantCommunityIn extranetVpnTenantCommunityIn);
    }
}
