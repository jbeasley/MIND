using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using SCM.Services;
using SCM.Models;
using SCM.Data;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for Tenant IP Networks
    /// </summary>
    public class TenantIpNetworkValidator : BaseValidator, ITenantIpNetworkValidator
    {
        public TenantIpNetworkValidator(IUnitOfWork unitOfWork): base(unitOfWork)
        {
        }

        /// <summary>
        /// Validate a tenant IP network can be deleted. The tenant IP network cannot be deleted if 
        /// the network is bound to a VPN.
        /// </summary>
        /// <param name="tenantIpNetworkId"></param>
        public async Task ValidateDeleteAsync(int tenantIpNetworkId)
        {
            (from result in await _unitOfWork.VpnTenantIpNetworkInRepository.GetAsync(q =>
            q.TenantIpNetwork.TenantIpNetworkID == tenantIpNetworkId,
            includeProperties:"TenantIpNetwork,AttachmentSet", AsTrackable: false)
            select result)
            .ToList()
            .ForEach(x => ValidationDictionary.AddError(string.Empty, $"Tenant IP network '{x.TenantIpNetwork.CidrName}' "
            + $"cannot be deleted because it is used in the inbound policy of attachment set '{x.AttachmentSet.Name}'."));

            (from result in await _unitOfWork.VpnTenantIpNetworkOutRepository.GetAsync(q =>
            q.TenantIpNetwork.TenantIpNetworkID == tenantIpNetworkId,
            includeProperties: "TenantIpNetwork,AttachmentSet", AsTrackable: false)
            select result)
            .ToList()
            .ForEach(x => ValidationDictionary.AddError(string.Empty, $"Tenant IP network '{x.TenantIpNetwork.CidrName}' "
            + $"cannot be deleted because it is used in the outbound policy of attachment set '{x.AttachmentSet.Name}'."));

            (from result in await _unitOfWork.VpnTenantIpNetworkRoutingInstanceRepository.GetAsync(q =>
            q.TenantIpNetwork.TenantIpNetworkID == tenantIpNetworkId,
            includeProperties: "TenantIpNetwork,AttachmentSet", AsTrackable: false)
             select result)
            .ToList()
            .ForEach(x => ValidationDictionary.AddError(string.Empty, $"Tenant IP network '{x.TenantIpNetwork.CidrName}' "
            + $"cannot be deleted because it is used in the routing instance policy of attachment set '{x.AttachmentSet.Name}'."));

            (from result in await _unitOfWork.VpnTenantIpNetworkStaticRouteRoutingInstanceRepository.GetAsync(q =>
            q.TenantIpNetwork.TenantIpNetworkID == tenantIpNetworkId,
            includeProperties: "TenantIpNetwork,AttachmentSet", AsTrackable: false)
             select result)
            .ToList()
            .ForEach(x => ValidationDictionary.AddError(string.Empty, $"Tenant IP network '{x.TenantIpNetwork.CidrName}' "
            + $"cannot be deleted because it is used in the static routing policy of attachment set '{x.AttachmentSet.Name}'."));
        }

        /// <summary>
        /// Validate changes to a tenant IP network.
        /// </summary>
        /// <param name="tenantIpNetwork"></param>
        public async Task ValidateChangesAsync(TenantIpNetwork tenantIpNetwork)
        {
            if (!tenantIpNetwork.AllowExtranet)
            {
                (from vpnTenantNetworksIn in await _unitOfWork.VpnTenantIpNetworkInRepository.GetAsync(q => 
                    q.TenantIpNetwork.TenantIpNetworkID == tenantIpNetwork.TenantIpNetworkID,
                    includeProperties:"AttachmentSet.VpnAttachmentSets.Vpn.ExtranetVpns.MemberVpn",
                    AsTrackable: false)
                    from vpnAttachmentSets in vpnTenantNetworksIn.AttachmentSet.VpnAttachmentSets
                    from result in vpnAttachmentSets.Vpn.ExtranetVpns
                    select result)
                    .ToList()
                    .ForEach(x => ValidationDictionary.AddError(string.Empty, "The 'Allow Extranet' attribute must be enabled for tenant network "
                        + $"'{tenantIpNetwork.CidrName}' because the network is bound to VPN '{x.MemberVpn.Name}' "
                        + $"which belongs to extranet VPN '{x.ExtranetVpn.Name}'."));
            }
        }
    }
}
