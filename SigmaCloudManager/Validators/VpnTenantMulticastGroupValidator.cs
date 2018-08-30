using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using SCM.Models;
using Mind.Services;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for Multicast VPN Groups.
    /// </summary>
    public class VpnTenantMulticastGroupValidator : BaseValidator, IVpnTenantMulticastGroupValidator
    {
        public VpnTenantMulticastGroupValidator(IVpnAttachmentSetService vpnAttachmentSetService,
            IAttachmentSetService attachmentSetService,
            ITenantMulticastGroupService tenantMulticastGroupService,
            IVpnTenantMulticastGroupService vpnTenantMulticastGroupService,
            IMulticastVpnRpService multicastVpnRpService)
        {
            VpnAttachmentSetService = vpnAttachmentSetService;
            AttachmentSetService = attachmentSetService;
            TenantMulticastGroupService = tenantMulticastGroupService;
            VpnTenantMulticastGroupService = vpnTenantMulticastGroupService;
            MulticastVpnRpService = multicastVpnRpService;
        }

        private IVpnAttachmentSetService VpnAttachmentSetService { get; }
        private IAttachmentSetService AttachmentSetService { get; }
        private ITenantMulticastGroupService TenantMulticastGroupService { get; }
        private IVpnTenantMulticastGroupService VpnTenantMulticastGroupService { get; }
        private IMulticastVpnRpService MulticastVpnRpService { get; }      

        /// <summary>
        /// Validate a new VPN Tenant Multicast Group
        /// </summary>
        /// <param name="vpnTenantMulticastGroup"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(VpnTenantMulticastGroup vpnTenantMulticastGroup)
        {
            var vpnAttachmentSets = await VpnAttachmentSetService.GetAllByAttachmentSetIDAsync(vpnTenantMulticastGroup.AttachmentSetID);
            var tenantMulticastGroup = await TenantMulticastGroupService.GetByIDAsync(vpnTenantMulticastGroup.TenantMulticastGroupID);
            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantMulticastGroup.AttachmentSetID);

            if (attachmentSet.MulticastVpnDomainTypeID == null)
            {
                ValidationDictionary.AddError(string.Empty, $"A Multicast Domain Type for Attachment Set '{attachmentSet.Name}' has not been defined. "
                    + "Edit the Attachment Set and select a Multicast Domain Type.");
                return;
            }

            if (attachmentSet.MulticastVpnDomainType.MvpnDomainType == MvpnDomainTypeEnum.ReceiverOnly)
            {
                ValidationDictionary.AddError(string.Empty, $"A Multicast Group range cannot be added to Attachment '{attachmentSet.Name}' Set because " 
                    + "the Multicast Domain Type of the Attachment Set is designated 'Receiver-Only'.");
            }

            if (tenantMulticastGroup.IsSsmGroup)
            {
                if (vpnTenantMulticastGroup.MulticastVpnRpID != null)
                {
                    ValidationDictionary.AddError(string.Empty, "A rendezvous-point cannot be selected for multicast group range "
                        + $"'{tenantMulticastGroup.Name}' because the group range is a source-specific multicast group range.");
                }
            }
            else
            {
                if (vpnTenantMulticastGroup.MulticastVpnRpID == null)
                {
                    ValidationDictionary.AddError(string.Empty, "A rendezvous-point must be selected because multicast group range "
                        + $"'{tenantMulticastGroup.Name}' is an any-source multicast group range.");
                }
            }
            
            var vpns = vpnAttachmentSets.Select(x => x.Vpn).Where(x => x.IsMulticastVpn);
            foreach (var vpn in vpns)
            {
                if (vpn.IsExtranet)
                {
                    if (!tenantMulticastGroup.AllowExtranet)
                    {
                        ValidationDictionary.AddError(string.Empty, $"Multicast group range "
                            + $"'{tenantMulticastGroup.Name}' must be enabled for Extranet because Attachment Set '{attachmentSet.Name}' belongs "
                            + $"to VPN '{vpn.Name}' which is an Extranet VPN.");
                    }
                }

                if (tenantMulticastGroup.IsSsmGroup)
                {
                    if (vpn.MulticastVpnServiceType.MvpnServiceType == MvpnServiceTypeEnum.ASM)
                    {
                        ValidationDictionary.AddError(string.Empty, $"Tenant Multicast Group range "
                            + $"'{tenantMulticastGroup.Name}' "
                            + $"is an SSM group range and cannot be added to Attachment Set '{attachmentSet.Name}' because the Attachment Set "
                            + $"belongs to VPN '{vpn.Name}' which is an ASM VPN service.");
                    }
                }
                else
                {
                    if (vpn.MulticastVpnServiceType.MvpnServiceType == MvpnServiceTypeEnum.SSM)
                    {
                        ValidationDictionary.AddError(string.Empty, $"Tenant Multicast Group range "
                            + $"'{tenantMulticastGroup.Name}' "
                            + $"is an ASM group range and cannot be added to Attachment Set '{attachmentSet.Name}' because the Attachment Set "
                            + $"belongs to VPN '{vpn.Name}' which is an SSM VPN service.");
                    }
                }
            }

            // The following logic is under debate and is currently commented out
            // This means that there is no logic which prevents the same tenant multicast group from appearing
            // in multiple multi-tenant/extranet vpns.

            /**

            if (vpn.VpnTenancyType.TenancyType != "Single")
            {
                var existingVpnTenantMulticastGroups =
                    await VpnTenantMulticastGroupService.GetAllByTenantMulticastGroupIDAsync(vpnTenantMulticastGroup.TenantMulticastGroupID);

                foreach (var existingVpnTenantMulticastGroup in existingVpnTenantMulticastGroups)
                {
                    var existingVpn = existingVpnTenantMulticastGroup.MulticastVpnRp.VpnAttachmentSet.Vpn;

                    if (existingVpn.IsExtranet)
                    {
                        ValidationDictionary.AddError(string.Empty, "Tenant Multicast Group "
                            + $"'{existingVpnTenantMulticastGroup.TenantMulticastGroup.GroupAddress}' "
                            + $"is already bound to Extranet VPN '{existingVpn.Name}'.");
                    }
                    else if (existingVpn.VpnTenancyType.TenancyType == "Multi")
                    {
                        ValidationDictionary.AddError(string.Empty, "Tenant Multicast Group "
                            + $"'{existingVpnTenantMulticastGroup.TenantMulticastGroup.GroupAddress}' "
                            + $"is already bound to Multi-Tenant VPN '{existingVpn.Name}'.");
                    }
                }
            }

            **/
        }

        /// <summary>
        /// Validate changes to a VPN Tenant Multicast Group
        /// </summary>
        /// <param name="vpnTenantMulticastGroup"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(VpnTenantMulticastGroup vpnTenantMulticastGroup)
        {
            var vpnAttachmentSets = await VpnAttachmentSetService.GetAllByAttachmentSetIDAsync(vpnTenantMulticastGroup.AttachmentSetID);
            var vpns = vpnAttachmentSets.Select(x => x.Vpn);
            var tenantMulticastGroup = await TenantMulticastGroupService.GetByIDAsync(vpnTenantMulticastGroup.TenantMulticastGroupID);

            foreach (var vpn in vpns)
            {
                if (vpn.IsExtranet)
                {
                    if (!tenantMulticastGroup.AllowExtranet)
                    {
                        ValidationDictionary.AddError(string.Empty, $"Tenant Multicast Group range "
                            + $"'{tenantMulticastGroup.Name}' must be enabled for Extranet because it is "
                            + $"associated with Extranet VPN '{vpn.Name}'.");
                    }
                }

                if (tenantMulticastGroup.IsSsmGroup)
                {
                    if (vpn.MulticastVpnServiceType.MvpnServiceType == MvpnServiceTypeEnum.ASM)
                    {
                        ValidationDictionary.AddError(string.Empty, $"Tenant Multicast Group range "
                            + $"'{tenantMulticastGroup.Name}' "
                            + $"is an SSM group range and cannot be added to ASM VPN '{vpn.Name}'.");
                    }
                }
                else
                {
                    if (vpn.MulticastVpnServiceType.MvpnServiceType == MvpnServiceTypeEnum.SSM)
                    {
                        ValidationDictionary.AddError(string.Empty, $"Tenant Multicast Group range "
                            + $"'{tenantMulticastGroup.Name}' is an ASM group range and cannot be added "
                            + $"to SSM VPN '{vpn.Name}'.");
                    }
                }
            }
        }
    }
}
