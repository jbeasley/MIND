using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using Mind.Models.RequestModels;

namespace SCM.Services
{
    public interface IProviderDomainCommunityInboundPolicyService : IBaseService
    {
        Task<IEnumerable<VpnTenantCommunityIn>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantCommunityIn> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<VpnTenantCommunityIn>> GetAllByVpnIDAsync(int vpnId, int? tenantId = null, bool extranet = false, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantCommunityIn> AddAsync(int attachmentSetId, VpnTenantCommunityInRequest request);
        Task<VpnTenantCommunityIn> UpdateAsync(int vpnTenantCommunityInId, VpnTenantCommunityInUpdate update);
        Task DeleteAsync(int vpnTenantCommunityInId);
    }
}
