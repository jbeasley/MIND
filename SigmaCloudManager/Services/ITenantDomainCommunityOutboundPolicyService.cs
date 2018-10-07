using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using Mind.Models.RequestModels;

namespace SCM.Services
{
    public interface ITenantDomainCommunityOutboundPolicyService : IBaseService
    {
        Task<IEnumerable<VpnTenantCommunityOut>> GetAllByDeviceIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantCommunityOut> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantCommunityOut> AddAsync(int deviceId, TenantDomainCommunityOutboundPolicyRequest request);
        Task<VpnTenantCommunityOut> UpdateAsync(int vpnTenantCommunityInId, TenantDomainCommunityOutboundPolicyUpdate update);
        Task DeleteAsync(int vpnTenantCommunityInId);
    }
}
