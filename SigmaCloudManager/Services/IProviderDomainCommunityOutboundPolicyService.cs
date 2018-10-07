using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using Mind.Models.RequestModels;

namespace SCM.Services
{
    public interface IProviderDomainCommunityOutboundPolicyService : IBaseService
    {
        Task<IEnumerable<VpnTenantCommunityOut>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<VpnTenantCommunityOut>> GetAllByVpnIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantCommunityOut> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantCommunityOut> AddAsync(int attachmentSetId, VpnTenantCommunityOutRequest request);
        Task<VpnTenantCommunityOut> UpdateAsync(int vpnTenantCommunityOutId, VpnTenantCommunityOutUpdate update);
        Task DeleteAsync(int vpnTenantNetworkOutId);
    }
}
