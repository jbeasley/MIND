using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using SCM.Models;
using SCM.Data;

namespace SCM.Validators

{
    /// <summary>
    /// Validator for Tenant Communities
    /// </summary>
    public class TenantCommunityValidator : BaseValidator, ITenantCommunityValidator
    {
        public TenantCommunityValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task ValidateNewAsync(TenantCommunity tenantCommunity)
        {
            var existsTenantCommunity = (from result in await _unitOfWork.TenantCommunityRepository.GetAsync(
                q =>
                    q.AutonomousSystemNumber == tenantCommunity.AutonomousSystemNumber
                    && q.Number == tenantCommunity.Number,
                    AsTrackable: false,
                    includeProperties: "Tenant")
                    select result)
                    .SingleOrDefault();

            if (existsTenantCommunity != null)
            {
                ValidationDictionary.AddError(string.Empty, $"Community {tenantCommunity.Name} "
                    + $"already exists for tenant {existsTenantCommunity.Tenant.Name}.");
            }
        }

        /// <summary>
        /// Validate deletion of a tenant community. The tenant community cannot be deleted if 
        /// the community is bound to an attachment set.
        /// </summary>
        /// <param name="tenantCommunity"></param>
        public async Task ValidateDeleteAsync(int tenantCommunityId)
        {
            var tenantCommunity = (from result in await _unitOfWork.TenantCommunityRepository.GetAsync(
                q =>
                    q.TenantCommunityID == tenantCommunityId,
                    includeProperties: "VpnTenantCommunitiesIn.AttachmentSet, " +
                    "VpnTenantCommunityOut.AttachmentSet," +
                    "VpnTenantCommunitiesRoutingInstance.AttachmentSet," +
                    "TenantCommunitySets.AttachmentSet," +
                    "VpnTenantIpNetworkCommunitiesIn.VpnTenantIpNetworkIn.TenantIpNetwork," +
                    "VpnTenantIpNetworkCommunitiesIn.VpnTenantIpNetworkIn.AttachmentSet",
                    AsTrackable: false)
                                   select result)
                                   .Single();

            (from vpnTenantCommunitiesIn in tenantCommunity.VpnTenantCommunitiesIn
             select vpnTenantCommunitiesIn)
             .ToList()
             .ForEach(x =>
                ValidationDictionary.AddError(string.Empty, $"Tenant community '{tenantCommunity.Name}' "
                    + $"cannot be deleted because it is used for inbound policy in attachment set '{x.AttachmentSet.Name}'.")
            );

            (from vpnTenantCommunitiesOut in tenantCommunity.VpnTenantCommunitiesOut
             select vpnTenantCommunitiesOut)
             .ToList()
             .ForEach(x =>
                ValidationDictionary.AddError(string.Empty, $"Tenant community '{tenantCommunity.Name}' "
                    + $"cannot be deleted because it is used for outbound policy in attachment set '{x.AttachmentSet.Name}'.")
            );

            (from vpnTenantCommunitiesRoutingInstance in tenantCommunity.VpnTenantCommunitiesRoutingInstance
             select vpnTenantCommunitiesRoutingInstance)
             .ToList()
             .ForEach(x =>
                ValidationDictionary.AddError(string.Empty, $"Tenant community '{tenantCommunity.Name}' "
                    + $"cannot be deleted because it is used for routing instance policy in attachment set '{x.AttachmentSet.Name}'.")
            );

            (from vpnTenantIpNetworkCommunities in tenantCommunity.VpnTenantIpNetworkCommunitiesIn
             select vpnTenantIpNetworkCommunities)
             .ToList()
             .ForEach(x =>
                ValidationDictionary.AddError(string.Empty, $"Tenant community '{tenantCommunity.Name}' "
                    + $"cannot be deleted because it is bound to tenant IP network '{x.VpnTenantIpNetworkIn.TenantIpNetwork.CidrName}' "
                    + $"in attachment set '{x.VpnTenantIpNetworkIn.AttachmentSet.Name}'.")
            );

            (from tenantCommunitySets in tenantCommunity.TenantCommunitySets
             select tenantCommunitySets)
             .ToList()
             .ForEach(x =>
                ValidationDictionary.AddError(string.Empty, $"Tenant community '{tenantCommunity.Name}' "
                    + $"cannot be deleted because it is bound to tenant community set '{x.Name}'.")
            );
        }

        /// <summary>
        /// Validate changes to a tenant community.
        /// </summary>
        /// <param name="tenantCommunity"></param>
        public async Task ValidateChangesAsync(TenantCommunity tenantCommunity)
        {
            if (!tenantCommunity.AllowExtranet)
            {
                (from vpnTenantCommunitiesIn in await _unitOfWork.VpnTenantCommunityInRepository.GetAsync(
                    q =>
                        q.TenantCommunityID == tenantCommunity.TenantCommunityID)
                        from vpnAttachmentSets in vpnTenantCommunitiesIn.AttachmentSet.VpnAttachmentSets
                            from result in vpnAttachmentSets.Vpn.ExtranetVpns
                            select result)
                            .ToList()
                            .ForEach(extranetVpnMember =>
                                ValidationDictionary.AddError(string.Empty, "The 'Allow Extranet' attribute must be enabled for tenant community "
                                + $"'{tenantCommunity.Name}' because the community is bound to VPN '{extranetVpnMember.MemberVpn.Name}' "
                                + $"which belongs to extranet VPN '{extranetVpnMember.ExtranetVpn.Name}'.")
                );
            }
        }
    }
}
