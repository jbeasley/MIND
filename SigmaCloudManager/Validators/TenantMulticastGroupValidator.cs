using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using SCM.Services;
using SCM.Models;

namespace SCM.Validators

{
    /// <summary>
    /// Validator for Tenant Communities
    /// </summary>
    public class TenantMulticastGroupValidator : BaseValidator, ITenantMulticastGroupValidator
    {
        public TenantMulticastGroupValidator(ITenantMulticastGroupService tenantMulticastGroupService, 
            IVpnTenantMulticastGroupService vpnTenantMulticastGroupService)
        {
            VpnTenantMulticastGroupService = vpnTenantMulticastGroupService;
            TenantMulticastGroupService = tenantMulticastGroupService;
        }

        private ITenantMulticastGroupService TenantMulticastGroupService { get; }
        private IVpnTenantMulticastGroupService VpnTenantMulticastGroupService { get; }

        public async Task ValidateNewAsync(TenantMulticastGroup tenantMulticastGroup)
        {
            // Check if the new Tenant Multicast Group range overlaps or is overlapped 
            // by a Tenant Multicast Group range owned by any other Tenant

            var tenantMulticastGroups = await TenantMulticastGroupService.GetAllAsync();
            foreach (var g in tenantMulticastGroups)
            {
                // Same Tenant can have overlapping multicast groups, different Tenants cannot

                if (g.TenantID != tenantMulticastGroup.TenantID)
                {
                    ValidateGroupRanges(tenantMulticastGroup, g);
                }
            }
        }

        /// <summary>
        /// Validate deletion of a Tenant Multicast Group. The Tenant Multicast Group cannot be deleted if 
        /// the group is bound to a VPN.
        /// </summary>
        /// <param name="tenantMulticastGroup"></param>
        public async Task ValidateDeleteAsync(TenantMulticastGroup tenantMulticastGroup)
        {
            var vpnTenantMulticastGroups = await VpnTenantMulticastGroupService.GetAllByTenantMulticastGroupIDAsync(tenantMulticastGroup.TenantMulticastGroupID);
            foreach (var vpnTenantMulticastGroup in vpnTenantMulticastGroups)
            {
                ValidationDictionary.AddError(string.Empty, $"Tenant Multicast Group '{tenantMulticastGroup.Name}' "
                    + $"cannot be deleted because it is bound to Attachment Set '{vpnTenantMulticastGroup.AttachmentSet.Name}'.");
            }
        }

        /// <summary>
        /// Validate changes to a Tenant Multicast Group.
        /// </summary>
        /// <param name="tenantMulticastGroup"></param>
        public async Task ValidateChangesAsync(TenantMulticastGroup tenantMulticastGroup)
        {
            var vpnTenantMulticastGroups = await VpnTenantMulticastGroupService.GetAllByTenantMulticastGroupIDAsync(tenantMulticastGroup.TenantMulticastGroupID);
            var vpnAttachmentSets = vpnTenantMulticastGroups
                .SelectMany(x => x.AttachmentSet.VpnAttachmentSets)
                .Where(x => x.Vpn.IsMulticastVpn);

            foreach (var vpnAttachmentSet in vpnAttachmentSets)
            {
                var vpn = vpnAttachmentSet.Vpn;
                var attachmentSet = vpnAttachmentSet.AttachmentSet;

                if (!tenantMulticastGroup.AllowExtranet)
                {
                    if (vpn.IsExtranet)
                    {
                        ValidationDictionary.AddError(string.Empty, "The 'Allow Extranet' attribute must be enabled for multicast group range "
                            + $"'{tenantMulticastGroup.Name}' because the group range belongs to Attachment Set '{attachmentSet.Name}' "
                            + $"which is bound to Extranet VPN '{vpn.Name}'.");
                    }
                }

                if (tenantMulticastGroup.IsSsmGroup)
                {
                    if (vpn.MulticastVpnServiceType.MvpnServiceType != MvpnServiceTypeEnum.SSM)
                    {
                        ValidationDictionary.AddError(string.Empty, "The 'SSM' attribute cannot be enabled for multicast group range "
                            + $"'{tenantMulticastGroup.Name}' because the group range belongs to Attachment Set '{attachmentSet.Name}' "
                            + $"which is bound to VPN '{vpn.Name}' which has a Multicast VPN Service Type of '{vpn.MulticastVpnServiceType.Name}'.");
                    }
                }
                else
                {
                    if (vpn.MulticastVpnServiceType.MvpnServiceType == MvpnServiceTypeEnum.SSM)
                    {
                        ValidationDictionary.AddError(string.Empty, "The 'SSM' attribute must be enabled for multicast group range "
                            + $"'{tenantMulticastGroup.Name}' because the group range belongs to Attachment Set '{attachmentSet.Name}' "
                            + $"which is bound to VPN '{vpn.Name}' which has a Multicast VPN Service Type of SSM.");
                    }
                }
            }
                
            // Check if the updated Tenant Multicast Group range conflicts 
            // with any other Multicast Group range owned by any other Tenant

            var tenantMulticastGroups = await TenantMulticastGroupService.GetAllAsync();

            foreach (var g in tenantMulticastGroups)
            {
                // Same Tenant can have overlapping multicast groups, different Tenants cannot

                if (g.TenantID != tenantMulticastGroup.TenantID)
                {
                    ValidateGroupRanges(tenantMulticastGroup, g);
                }
            }
        }

        /// <summary>
        /// Validates if one multicast group range overlaps with another multicast group range.
        /// </summary>
        /// <param name="tenantMulticastGroup1"></param>
        /// <param name="tenantMulticsatGroup2"></param>
        /// <returns></returns>
        private void ValidateGroupRanges(TenantMulticastGroup tenantMulticastGroup1, TenantMulticastGroup tenantMulticastGroup2)
        {
            IPNetwork groupRange1;
            if (!IPNetwork.TryParse(tenantMulticastGroup1.GroupAddress, tenantMulticastGroup1.GroupMask, out groupRange1)) {

                throw new ValidationFailureException($"Error parsing Group address range '{tenantMulticastGroup1.Name}'.");
            }

            IPNetwork groupRange2;
            if (!IPNetwork.TryParse(tenantMulticastGroup2.GroupAddress, tenantMulticastGroup2.GroupMask, out groupRange2)) {

                throw new ValidationFailureException($"Error parsing Group address range '{tenantMulticastGroup2.Name}'.");
            }

            bool overlaps = false;

            if (IPNetwork.Overlap(groupRange1, groupRange2))
            {
                // Group ranges overlap 

                if (tenantMulticastGroup1.IsSsmGroup && tenantMulticastGroup2.IsSsmGroup)
                {
                    // Both group ranges to be tested are SSM so check if the source address
                    // ranges overlap. If they do then the SSM group ranges are in conflict.

                    var sourceRange1 = IPNetwork.Parse(tenantMulticastGroup1.SourceAddress, tenantMulticastGroup1.SourceMask);
                    var sourceRange2 = IPNetwork.Parse(tenantMulticastGroup2.SourceAddress, tenantMulticastGroup2.SourceMask);

                    overlaps = IPNetwork.Overlap(sourceRange1, sourceRange2);
                }
                else
                {
                    // Either both groups ranges are ASM or one is ASM and the other is SSM
                    // The group ranges must not be in conflict in any of these cases

                    overlaps = true;
                }
            }

            if (overlaps)
            {
                ValidationDictionary.AddError(string.Empty, $"Group address range '{tenantMulticastGroup1.Name}' "
                    + $"overlaps with '{tenantMulticastGroup2.Name}' which is owned by Tenant '{tenantMulticastGroup2.Tenant.Name}'.");
            }
        }
    }
}
