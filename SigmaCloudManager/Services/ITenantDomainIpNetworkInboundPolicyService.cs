using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using Mind.Models.RequestModels;

namespace SCM.Services
{
    public interface ITenantDomainIpNetworkInboundPolicyService : IBaseService
    {
        Task<IEnumerable<VpnTenantIpNetworkIn>> GetAllByDeviceIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkIn> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<VpnTenantIpNetworkIn> AddAsync(int deviceId, TenantDomainIpNetworkInboundPolicyRequest request);
        Task<VpnTenantIpNetworkIn> UpdateAsync(int vpnTenantIpNetworkInId, TenantDomainIpNetworkInboundPolicyUpdate update);
        Task DeleteAsync(int vpnTenantIpNetworkInId);
    }
}
