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
    /// Validator for VPN Attachment Sets (Attachment Sets which are bound to a VPN).
    /// </summary>
    public class VpnAttachmentSetValidator : BaseValidator, IVpnAttachmentSetValidator
    {
        public VpnAttachmentSetValidator(IVpnService vpnService,
            IAttachmentSetService attachmentSetService,
            IMulticastVpnDomainTypeService multicastVpnDomainTypeService,
            IAttachmentSetRoutingInstanceValidator attachmentSetRoutingInstanceValidator)
        {
            VpnService = vpnService;
            MulticastVpnDomainTypeService = multicastVpnDomainTypeService;
            AttachmentSetService = attachmentSetService;
            AttachmentSetRoutingInstanceValidator = attachmentSetRoutingInstanceValidator;
        }

        private IVpnService VpnService { get; }
        private IMulticastVpnDomainTypeService MulticastVpnDomainTypeService { get; }
        private IAttachmentSetService AttachmentSetService { get; }
        private IAttachmentSetRoutingInstanceValidator AttachmentSetRoutingInstanceValidator { get; }

        public new IValidationDictionary ValidationDictionary
        {
            get
            {
                return base.ValidationDictionary;
            }
            set
            {
                base.ValidationDictionary = value;
                AttachmentSetRoutingInstanceValidator.ValidationDictionary = value;
            }
        }

        /// <summary>
        /// Validate a new VPN Attachment Set.
        /// </summary>
        /// <param name="vpnAttachmentSet"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(VpnAttachmentSet vpnAttachmentSet)
        {
            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnAttachmentSet.AttachmentSetID);
            var vpn = await VpnService.GetByIDAsync(vpnAttachmentSet.VpnID);
            var attachmentRedundancy = attachmentSet.AttachmentRedundancy;

            if (vpn.IsExtranet)
            {
                ValidationDictionary.AddError(string.Empty, "An Attachment Set cannot be added to an Extranet VPN. Instead, add VPNs as members "
                    + "to the Extranet VPN and then add communities and networks from the member VPN to the Extranet.");
                return;
            }

            if (vpn.Plane != null)
            {
                if (attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyType.Silver || 
                    attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyType.Gold)
                {
                    ValidationDictionary.AddError(string.Empty, $"A '{attachmentRedundancy.Name}' Attachment Set cannot be used "
                        + "with a planar-scoped VPN. "
                        + $"The VPN is scoped to the '{vpn.Plane.Name}' plane. "
                        + "The Attachment Set provides connectivity into both planes.");
                }

                else if (attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyType.Bronze)
                {
                    var attachmentPlane = attachmentSet.AttachmentSetRoutingInstances.Single().RoutingInstance.Device.Plane.Name;
                    if (attachmentPlane != vpn.Plane.Name)
                    {
                        ValidationDictionary.AddError(string.Empty, $"'{attachmentRedundancy.Name}' attachment '{attachmentSet.Name}' "
                        + $"cannot be used with VPN '{vpn.Name}' because the VPN is scoped to the '{vpn.Plane.Name}' plane. "
                        + $"The Attachment Set provides connectivity into the '{attachmentPlane}' plane.");
                    }
                }

                else if (attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyType.Custom)
                {
                    if (attachmentSet.AttachmentSetRoutingInstances.Where(v => v.RoutingInstance.Device.Plane.Name == vpn.Plane.Name).Count() != 
                        attachmentSet.AttachmentSetRoutingInstances.Count())
                    {
                        ValidationDictionary.AddError(string.Empty, $"The VPN is scoped to the '{vpn.Plane.Name}' plane. "
                            + "One or more VRFs in the Attachment Set are not located in this plane.");
                    }
                }
            }

            // Check to validate the Attachment Set for a Multicast VPN

            if (vpn.IsMulticastVpn)
            {
                await ValidateMulticast(vpn, attachmentSet, vpnAttachmentSet);
            }

            // Validate routing instances for the attachment set are configured correctly

            await AttachmentSetRoutingInstanceValidator.ValidateRoutingInstancesForAttachmentSetAsync(attachmentSet.AttachmentSetID);
        }

        /// <summary>
        /// Validate changes to a VPN Attachment Set.
        /// </summary>
        /// <param name="vpnAttachmentSet"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(VpnAttachmentSet vpnAttachmentSet)
        {
            var vpn = await VpnService.GetByIDAsync(vpnAttachmentSet.VpnID);
            if (vpn.IsMulticastVpn)
            {
                var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnAttachmentSet.AttachmentSetID);
                await ValidateMulticast(vpn, attachmentSet, vpnAttachmentSet);
            }
        }

        /// <summary>
        /// Helper to validate an Attachment Set for a Multicast VPN
        /// </summary>>
        /// <param name="vpnAttachmentSet"></param>
        /// <returns></returns>
        private async Task ValidateMulticast(Vpn vpn, AttachmentSet attachmentSet, VpnAttachmentSet vpnAttachmentSet)
        {

            // Multiport Attachments or VIFs which belong to Multiport Attachments do not support Multicast VPN.
            // This is a limitation due to network certification.

            foreach (var attachmentSetRoutingInstance in attachmentSet.AttachmentSetRoutingInstances)
            {
                if (attachmentSetRoutingInstance.RoutingInstance.Attachments
                   .Where(x => x.IsMultiPort).Any())
                {
                    ValidationDictionary.AddError(string.Empty, $"VRF '{attachmentSetRoutingInstance.RoutingInstance.Name}' which "
                        + $"belongs to this Attachment Set is bound to MultiPort Attachment " 
                        + $"'{attachmentSetRoutingInstance.RoutingInstance.Attachments.Single().Name}'. "
                        + "Multiport Attachments do not support Multicast VPN.");
                }

                if (attachmentSetRoutingInstance.RoutingInstance.Vifs
                   .Select(x => x.Attachment)
                   .Where(x => x.IsMultiPort).Any())
                {
                    var vif = attachmentSetRoutingInstance.RoutingInstance.Vifs.Single();
                    ValidationDictionary.AddError(string.Empty, $"VRF '{attachmentSetRoutingInstance.RoutingInstance.Name}' which belongs "
                        + $"to this Attachment Set is bound to VIF '{vif.Name}' which is associated with MultiPort Attachment "
                        + $"'{vif.Attachment.Name}'."
                        + "VIFs which are associated with Multiport Attachments do not support Multicast VPN.");
                }
            }

            if (attachmentSet.MulticastVpnDomainTypeID == null)
            {
                ValidationDictionary.AddError(string.Empty, $"Attachment Set '{attachmentSet.Name}' requires a Multicast Domain Type selection.");
                return;
            }

            var multicastVpnDomainType = await MulticastVpnDomainTypeService.GetByIDAsync(attachmentSet.MulticastVpnDomainTypeID.Value);
            if (multicastVpnDomainType == null)
            {
                ValidationDictionary.AddError(string.Empty, $"The Multicast VPN Domain Type selection for Attachment Set '{attachmentSet.Name}' was not found.");
                return;
            }

            var vpnTopologyType = vpn.VpnTopologyType.TopologyType;

            if (vpnTopologyType == TopologyType.HubandSpoke)
            {
                var multicastVpnDirectionType = vpn.MulticastVpnDirectionType.MvpnDirectionType;
                if (multicastVpnDirectionType == MvpnDirectionType.Unidirectional)
                {
                    if (vpnAttachmentSet.IsHub.GetValueOrDefault())
                    {
                        if (multicastVpnDomainType.MvpnDomainType != MvpnDomainType.SenderOnly)
                        {
                            ValidationDictionary.AddError(string.Empty, $"VPN '{vpn.Name}' is a Unidirectional Hub-and-Spoke Multicast VPN and "
                                + $"therefore the Multicast VPN Domain Type selection for Attachment Set '{attachmentSet.Name}' must be 'Sender-Only' "
                                + "because the Attachment Set is designated as a HUB.");
                        }
                    }
                    else
                    {
                        if (multicastVpnDomainType.MvpnDomainType != MvpnDomainType.ReceiverOnly)
                        {
                            ValidationDictionary.AddError(string.Empty, $"VPN '{vpn.Name}' is a Unidirectional Hub-and-Spoke Multicast VPN and "
                                + $"therefore the Multicast VPN Domain Type selection for Attachment Set '{attachmentSet.Name}' must be 'Receiver-Only' "
                                + "because the Attachment Set is designated as a SPOKE.");
                        }
                    }
                }
                else
                {
                    if (vpnAttachmentSet.IsHub.GetValueOrDefault())
                    {
                        if (multicastVpnDomainType.MvpnDomainType != MvpnDomainType.SenderOnly && 
                            multicastVpnDomainType.MvpnDomainType != MvpnDomainType.SenderAndReceiver)
                        {
                            ValidationDictionary.AddError(string.Empty, $"VPN '{vpn.Name}' is a Bidirectional Hub-and-Spoke Multicast VPN and "
                                + $"therefore the Multicast VPN Domain Type selection for Attachment Set '{attachmentSet.Name}' must be either "
                                + "'Sender-Only' or 'Sender-and-Receiver' because the Attachment Set is designated as a HUB.");
                        }
                    }
                    else
                    {
                        if (multicastVpnDomainType.MvpnDomainType != MvpnDomainType.ReceiverOnly && 
                            multicastVpnDomainType.MvpnDomainType != MvpnDomainType.SenderAndReceiver)
                        {
                            ValidationDictionary.AddError(string.Empty, $"VPN '{vpn.Name}' is a Bidirectional Hub-and-Spoke Multicast VPN and "
                                + $"therefore the Multicast VPN Domain Type selection for Attachment Set '{attachmentSet.Name}' must be "
                                + "'Receiver-Only' or 'Sender-and-Receiver' because the Attachment Set is designated as a SPOKE.");
                        }
                    }
                }
            }

            if (vpn.MulticastVpnServiceType.MvpnServiceType == MvpnServiceType.ASM)
            {
                if (attachmentSet.VpnTenantMulticastGroups.Any(x => x.TenantMulticastGroup.IsSsmGroup))
                {
                    ValidationDictionary.AddError(string.Empty, $"Attachment Set '{attachmentSet.Name}' cannot be added to VPN '{vpn.Name}' because " 
                        + "there are source-specific multicast group ranges associated with the Attachment Set and the Multicast Service Type of the VPN "
                        + " is ASM. Remove the SSM group ranges from the Attachment Set, or create a new Attachment Set.");
                }
            }

            else if (vpn.MulticastVpnServiceType.MvpnServiceType == MvpnServiceType.SSM)
            {
                if (attachmentSet.VpnTenantMulticastGroups.Any(x => !x.TenantMulticastGroup.IsSsmGroup))
                {
                    ValidationDictionary.AddError(string.Empty, $"Attachment Set '{attachmentSet.Name}' cannot be added to VPN '{vpn.Name}' because "
                        + "there are any-source multicast group ranges associated with the Attachment Set and the Multicast Service Type of the VPN "
                        + " is SSM. Remove the ASM group ranges from the Attachment Set, or create a new Attachment Set.");
                }
            }
        }
    }
}
