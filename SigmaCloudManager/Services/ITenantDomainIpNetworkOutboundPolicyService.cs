using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using Mind.Models.RequestModels;

namespace SCM.Services
{
    public interface ITenantDomainIpNetworkOutboundPolicyService : IBaseService
    {
        Task<IEnumerable<VpnTenantIpNetworkOut>> GetAllByDeviceIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkOut> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkOut> AddAsync(int deviceId, TenantDomainIpNetworkOutboundPolicyRequest request);
        Task<VpnTenantIpNetworkOut> UpdateAsync(int vpnTenantIpNetworkInId, TenantDomainIpNetworkOutboundPolicyUpdate update);
        Task DeleteAsync(int vpnTenantIpNetworkInId);
    }
}
