using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models.RequestModels;
using SCM.Services;
using SCM.Models;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for Attachment Sets
    /// </summary>
    public class AttachmentSetValidator : BaseValidator, IAttachmentSetValidator
    {
        public AttachmentSetValidator(IAttachmentSetService attachmentSetService,
            ISubRegionService subRegionService,
            IVpnAttachmentSetService vpnAttachmentSetService,
            IAttachmentRedundancyService attachmentRedundancyService,
            IMulticastVpnDomainTypeService multicastVpnDomainTypeService,
            IVpnTenantMulticastGroupService vpnTenantMulticastGroupService)
        {
            AttachmentSetService = attachmentSetService;
            SubRegionService = subRegionService;
            VpnAttachmentSetService = vpnAttachmentSetService;
            AttachmentRedundancyService = attachmentRedundancyService;
            VpnTenantMulticastGroupService = vpnTenantMulticastGroupService;
            MulticastVpnDomainTypeService = multicastVpnDomainTypeService;
        }

        private IAttachmentSetService AttachmentSetService { get; set; }
        private ISubRegionService SubRegionService { get; set; }
        private IVpnAttachmentSetService VpnAttachmentSetService { get; set; }
        private IAttachmentRedundancyService AttachmentRedundancyService { get; set; }
        private IMulticastVpnDomainTypeService MulticastVpnDomainTypeService { get; }
        private IVpnTenantMulticastGroupService VpnTenantMulticastGroupService { get; }

        /// <summary>
        /// Validate a new Attachment Set
        /// </summary>
        /// <param name="attachmentSet"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(AttachmentSet attachmentSet)
        {
            // Validate Attachment Redundancy

            await ValidateAttachmentRedundancy(attachmentSet);

            // If a Sub-Region is specified then the Sub-Region must be associated with the specified Region

            if (attachmentSet.SubRegionID != null)
            {
                var subRegion = await SubRegionService.GetByIDAsync(attachmentSet.SubRegionID.Value);
                if (attachmentSet.RegionID != subRegion.RegionID)
                {
                    ValidationDictionary.AddError(string.Empty, $"Sub-Region '{subRegion.Name}' is not valid "
                        + $" with Region '{attachmentSet.Region.Name}'.");
                }
            }
        }

        /// <summary>
        /// Validate that an Attachment Set can be deleted.
        /// </summary>
        /// <param name="attachmentSet"></param>
        /// <returns></returns>
        public async Task ValidateDeleteAsync(AttachmentSet attachmentSet)
        {
            var vpnAttachmentSets = await VpnAttachmentSetService.GetAllByAttachmentSetIDAsync(attachmentSet.AttachmentSetID);
            if (vpnAttachmentSets.Count() > 0)
            {
                vpnAttachmentSets.ToList().ForEach(q => ValidationDictionary.AddError(string.Empty, "First remove the Attachment Set "
                    + $"from VPN '{q.Vpn.Name}'"));
            }
        }

        /// <summary>
        /// Validate changes to an Attachment Set
        /// </summary>
        /// <param name="attachmentSet"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(AttachmentSet attachmentSet)
        {
            // Get the current Attachment Set

            var currentAttachmentSet = await AttachmentSetService.GetByIDAsync(attachmentSet.AttachmentSetID);
            if (currentAttachmentSet.AttachmentSetRoutingInstances.Any())
            {
                if (attachmentSet.AttachmentRedundancyID != currentAttachmentSet.AttachmentRedundancyID)
                {
                    ValidationDictionary.AddError(string.Empty, "The Attachment Redundancy option cannot be changed "
                        + "because VRFs are defined.");
                }

                if (attachmentSet.RegionID != currentAttachmentSet.RegionID)
                {
                    ValidationDictionary.AddError(string.Empty, "The Region cannot be changed because VRFs are defined.");
                }

                if (attachmentSet.SubRegionID != currentAttachmentSet.SubRegionID)
                {
                    ValidationDictionary.AddError(string.Empty, "The Sub-Region cannot be changed because VRFs are defined.");
                }

                if (attachmentSet.TenantID != currentAttachmentSet.TenantID)
                {
                    ValidationDictionary.AddError(string.Empty, "The Tenant cannot be changed because VRFs are defined.");
                }

                if (attachmentSet.IsLayer3 != currentAttachmentSet.IsLayer3)
                {
                    ValidationDictionary.AddError(string.Empty, "The Layer 3 property cannot be changed because VRFs are defined.");
                }
            }

            var vpnAttachmentSets = await VpnAttachmentSetService.GetAllByAttachmentSetIDAsync(attachmentSet.AttachmentSetID);
            foreach (var vpnAttachmentSet in vpnAttachmentSets)
            {
                var vpn = vpnAttachmentSet.Vpn;

                if (attachmentSet.MulticastVpnDomainTypeID == null)
                {
                    if (vpn.IsMulticastVpn)
                    {
                        ValidationDictionary.AddError(string.Empty, "A Multicast VPN Domain Type selection is required because the Attachment Set is "
                            + $"bound to Multicast VPN '{vpn.Name}'.");
                    }
                }

                if (attachmentSet.MulticastVpnDomainTypeID != null)
                {
                    var multicastVpnDomainType = await MulticastVpnDomainTypeService.GetByIDAsync(attachmentSet.MulticastVpnDomainTypeID.Value);
                    if (multicastVpnDomainType == null)
                    {
                        ValidationDictionary.AddError(string.Empty, "The Multicast VPN Domain Type selection was not found.");
                        return;
                    }

                    if (vpn.IsMulticastVpn)
                    {
                        if (vpn.VpnTopologyType.TopologyType == TopologyType.HubandSpoke)
                        {
                            if (vpn.MulticastVpnDirectionType.MvpnDirectionType == MvpnDirectionType.Unidirectional)
                            {
                                if (vpnAttachmentSet.IsHub.GetValueOrDefault())
                                {
                                    if (multicastVpnDomainType.MvpnDomainType != MvpnDomainType.SenderOnly)
                                    {
                                        ValidationDictionary.AddError(string.Empty, $"The Multicast VPN Domain Type selection of '{multicastVpnDomainType.Name}' is not "
                                            + $"valid because Attachment Set '{currentAttachmentSet.Name}' is designated as a HUB for hub-and-spoke multicast VPN "
                                            + $"'{vpn.Name}'. The Multicast Direction Type setting of the VPN is '{vpn.MulticastVpnDirectionType.Name}' and therefore "
                                            + "the Multicast VPN Domain Type of the Attachment Set must be 'Sender-Only'.");
                                    }
                                }
                                else
                                {
                                    if (multicastVpnDomainType.MvpnDomainType != MvpnDomainType.ReceiverOnly)
                                    {
                                        ValidationDictionary.AddError(string.Empty, $"The Multicast VPN Domain Type selection of '{multicastVpnDomainType.Name}' is not "
                                            + $"valid because Attachment Set '{currentAttachmentSet.Name}' is designated as a SPOKE for hub-and-spoke multicast VPN "
                                            + $"'{vpn.Name}'. The Multicast Direction Type setting of the VPN is '{vpn.MulticastVpnDirectionType.Name}' and therefore "
                                            + "the Multicast VPN Domain Type of the Attachment Set must be 'Receiver-Only'.");
                                    }
                                }
                            }
                            else
                            {
                                if (vpnAttachmentSet.IsHub.GetValueOrDefault())
                                {
                                    if (multicastVpnDomainType.MvpnDomainType != MvpnDomainType.SenderOnly
                                        && multicastVpnDomainType.MvpnDomainType != MvpnDomainType.SenderAndReceiver)
                                    {
                                        ValidationDictionary.AddError(string.Empty, $"The Multicast VPN Domain Type selection of '{multicastVpnDomainType.Name}' is not "
                                            + $"valid because Attachment Set '{currentAttachmentSet.Name}' is designated as a HUB for hub-and-spoke multicast VPN "
                                            + $"'{vpn.Name}'. The Multicast Direction Type setting of the VPN is '{vpn.MulticastVpnDirectionType.Name}' and therefore "
                                            + "the Multicast VPN Domain Type of the Attachment Set must be either 'Sender-and-Receiver' or 'Sender-Only'.");
                                    }
                                }
                                else
                                {
                                    if (multicastVpnDomainType.MvpnDomainType != MvpnDomainType.ReceiverOnly
                                        && multicastVpnDomainType.MvpnDomainType != MvpnDomainType.SenderAndReceiver)
                                    {
                                        ValidationDictionary.AddError(string.Empty, $"The Multicast VPN Domain Type selection of '{multicastVpnDomainType.Name}' is not "
                                            + $"valid because Attachment Set '{currentAttachmentSet.Name}' is designated as a SPOKE for hub-and-spoke multicast VPN "
                                            + $"'{vpn.Name}'. The Multicast Direction Type setting of the VPN is '{vpn.MulticastVpnDirectionType.Name}' and therefore "
                                            + "the Multicast VPN Domain Type of the Attachment Set must be either 'Sender-and-Receiver' or 'Receiver-Only'.");
                                    }
                                }
                            }
                        }            

                        if (multicastVpnDomainType.MvpnDomainType == MvpnDomainType.ReceiverOnly)
                        {
                            var tenantMulticastGroups = await VpnTenantMulticastGroupService.GetAllByAttachmentSetIDAsync(attachmentSet.AttachmentSetID);
                            if (tenantMulticastGroups.Any())
                            {
                                ValidationDictionary.AddError(string.Empty, "The Multicast Domain Type cannot be 'Receiver-Only' for Attachment Set "
                                    + $"'{currentAttachmentSet.Name}' because multicast group ranges are associated with the Attachment Set. "
                                    + "Remove the multicast group ranges from the Attachment Set first.");
                            }
                        }
                    }
                }
            }


            // Validate the Attachment Redundancy

            await ValidateAttachmentRedundancy(attachmentSet);
        }

        /// <summary>
        /// Validate the Attachment Redundancy option for an Attachment Set. 
        /// </summary>
        /// <param name="attachmentSet"></param>
        /// <returns></returns>
        private async Task ValidateAttachmentRedundancy(AttachmentSet attachmentSet)
        {
            var attachmentRedundancy = await AttachmentRedundancyService.GetByIDAsync(attachmentSet.AttachmentRedundancyID);
            if (attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyType.Gold)
            {
                if (attachmentSet.SubRegionID == null)
                {
                    ValidationDictionary.AddError(string.Empty, "A Sub-Region must be specified for "
                        + $"'{attachmentRedundancy.Name}' Attachment Sets.");
                }
            }
        }
    }
}
