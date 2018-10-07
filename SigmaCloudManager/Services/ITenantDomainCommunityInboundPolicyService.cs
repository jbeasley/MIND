using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using Mind.Models.RequestModels;

namespace SCM.Services
{
    public interface ITenantDomainCommunityInboundPolicyService : IBaseService
    {
        Task<IEnumerable<VpnTenantCommunityIn>> GetAllByDeviceIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantCommunityIn> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantCommunityIn> AddAsync(int deviceId, TenantDomainCommunityInboundPolicyRequest request);
        Task<VpnTenantCommunityIn> UpdateAsync(int vpnTenantCommunityInId, TenantDomainCommunityInboundPolicyUpdate update);
        Task DeleteAsync(int vpnTenantCommunityInId);
    }
}
