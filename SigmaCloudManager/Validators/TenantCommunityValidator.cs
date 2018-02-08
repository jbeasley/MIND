using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using SCM.Models;

namespace SCM.Validators

{
    /// <summary>
    /// Validator for Tenant Communities
    /// </summary>
    public class TenantCommunityValidator : BaseValidator, ITenantCommunityValidator
    {
        public TenantCommunityValidator(ITenantCommunityService tenantCommunityService, 
            IVpnTenantCommunityInService vpnTenantCommunityInService,
            IVpnTenantCommunityOutService vpnTenantCommunityOutService,
            IVpnTenantCommunityRoutingInstanceService vpnTenantCommunityRoutingInstanceService,
            IVpnTenantNetworkCommunityInService vpnTenantNetworkCommunityInService,
            ITenantCommunitySetService tenantCommunitySetService)
        {
            VpnTenantCommunityInService = vpnTenantCommunityInService;
            VpnTenantCommunityOutService = vpnTenantCommunityOutService;
            VpnTenantCommunityRoutingInstanceService = vpnTenantCommunityRoutingInstanceService;
            VpnTenantNetworkCommunityInService = vpnTenantNetworkCommunityInService;
            TenantCommunitySetService = tenantCommunitySetService;
            TenantCommunityService = tenantCommunityService;
        }

        private ITenantCommunityService TenantCommunityService { get; }
        private IVpnTenantCommunityInService VpnTenantCommunityInService { get; }
        private IVpnTenantCommunityOutService VpnTenantCommunityOutService { get; }
        private IVpnTenantCommunityRoutingInstanceService VpnTenantCommunityRoutingInstanceService { get; }
        private IVpnTenantNetworkCommunityInService VpnTenantNetworkCommunityInService { get; }
        private ITenantCommunitySetService TenantCommunitySetService { get; }

        public async Task ValidateNewAsync(TenantCommunity tenantCommunity)
        {
            var existsTenantCommunity = await TenantCommunityService.GetByCommunityAsync(tenantCommunity.AutonomousSystemNumber, tenantCommunity.Number);
            if (existsTenantCommunity != null)
            {
                ValidationDictionary.AddError(string.Empty, $"Community {tenantCommunity.Name} "
                    + $"already exists for Tenant {existsTenantCommunity.Tenant.Name}.");
            }
        }

        /// <summary>
        /// Validate deletion of a Tenant Community. The Tenant Community cannot be deleted if 
        /// the community is bound in any way to an Attachment Set.
        /// </summary>
        /// <param name="tenantCommunity"></param>
        public async Task ValidateDeleteAsync(TenantCommunity tenantCommunity)
        {
            var vpnTenantCommunitiesIn = await VpnTenantCommunityInService.GetAllByTenantCommunityIDAsync(tenantCommunity.TenantCommunityID);
            foreach (var vpnTenantCommunityIn in vpnTenantCommunitiesIn)
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant Community '{tenantCommunity.Name}' "
                    + $"cannot be deleted because it is used for Inbound Policy in Attachment Set '{vpnTenantCommunityIn.AttachmentSet.Name}'.");
            }

            var vpnTenantCommunitiesOut = await VpnTenantCommunityOutService.GetAllByTenantCommunityIDAsync(tenantCommunity.TenantCommunityID);
            foreach (var vpnTenantCommunityOut in vpnTenantCommunitiesOut)
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant Community '{tenantCommunity.Name}' "
                    + $"cannot be deleted because it is used for Outbound Policy in Attachment Set '{vpnTenantCommunityOut.AttachmentSet.Name}'.");
            }

            var vpnTenantCommunitiesRoutingInstance = await VpnTenantCommunityRoutingInstanceService.GetAllByTenantCommunityIDAsync(tenantCommunity.TenantCommunityID);
            foreach (var vpnTenantCommunityRoutingInstance in vpnTenantCommunitiesRoutingInstance)
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant Community '{tenantCommunity.Name}' "
                    + $"cannot be deleted because it is used for VRF Policy in Attachment Set '{vpnTenantCommunityRoutingInstance.AttachmentSet.Name}'.");
            }

            var vpnTenantNetworkCommunities = await VpnTenantNetworkCommunityInService.GetAllByTenantCommunityIDAsync(tenantCommunity.TenantCommunityID);
            foreach (var vpnTenantNetworkCommunityIn in vpnTenantNetworkCommunities)
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant Community '{tenantCommunity.Name}' "
                    + $"cannot be deleted because it is bound to Tenant Network '{vpnTenantNetworkCommunityIn.VpnTenantNetworkIn.TenantNetwork.CidrName}' "
                    + $"in Attachment Set '{vpnTenantNetworkCommunityIn.VpnTenantNetworkIn.AttachmentSet.Name}'.");
            }

            var tenantCommunitySets = await TenantCommunitySetService.GetAllByTenantCommunityIDAsync(tenantCommunity.TenantCommunityID);
            foreach (var tenantCommunitySet in tenantCommunitySets)
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant Community '{tenantCommunity.Name}' "
                    + $"cannot be deleted because it is bound to Tenant Community Set '{tenantCommunitySet.Name}'.");
            }
        }

        /// <summary>
        /// Validate changes to a Tenant Community.
        /// </summary>
        /// <param name="tenantCommunity"></param>
        public async Task ValidateChangesAsync(TenantCommunity tenantCommunity)
        {
            if (!tenantCommunity.AllowExtranet)
            {
                var query = from vpnTenantCommunitiesIn in await VpnTenantCommunityInService.GetAllByTenantCommunityIDAsync(tenantCommunity.TenantCommunityID)
                                from vpnAttachmentSets in vpnTenantCommunitiesIn.AttachmentSet.VpnAttachmentSets
                                   from e in vpnAttachmentSets.Vpn.ExtranetVpns
                                       select e;

                var extranetVpnMembers = query.ToList();

                foreach (var extranetVpnMember in extranetVpnMembers)
                {
                    ValidationDictionary.AddError(string.Empty, "The 'Allow Extranet' attribute must be enabled for Tenant Community "
                        + $"'{tenantCommunity.Name}' because the community is bound to VPN '{extranetVpnMember.MemberVpn.Name}' "
                        + $"which belongs to Extranet VPN '{extranetVpnMember.ExtranetVpn.Name}'.");
                }
            }
        }
    }
}
