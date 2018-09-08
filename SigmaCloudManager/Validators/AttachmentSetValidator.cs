using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models.RequestModels;
using SCM.Services;
using SCM.Models;
using SCM.Data;
using Mind.Models.RequestModels;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for Attachment Sets
    /// </summary>
    public class AttachmentSetValidator : BaseValidator, IAttachmentSetValidator
    {
        public AttachmentSetValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Validate that an attachment set can be deleted.
        /// </summary>
        /// <param name="attachmentSetId"></param>
        /// <returns></returns>
        public async Task ValidateDeleteAsync(int attachmentSetId)
        {
            var vpnAttachmentSets = (from attachmentSets in await _unitOfWork.VpnAttachmentSetRepository.GetAsync(q =>
                                     q.AttachmentSet.AttachmentSetID == attachmentSetId)
                                     select attachmentSets)
                                     .ToList();

            if (vpnAttachmentSets.Any()) vpnAttachmentSets.ForEach(q =>
            ValidationDictionary.AddError(string.Empty, $"First remove the Attachment Set from VPN '{q.Vpn.Name}'"));
        }

        /// <summary>
        /// Validate changes to an attachment set
        /// </summary>
        /// <param name="attachmentSetId"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(int attachmentSetId, AttachmentSetUpdate update)
        {
            var attachmentSet = (from attachmentSets in await _unitOfWork.AttachmentSetRepository.GetAsync(q => q.AttachmentSetID == attachmentSetId,
                includeProperties: "AttachmentSetRoutingInstances,VpnAttachmentSets.Vpn")
                                 select attachmentSets)
                                 .Single();

            if (attachmentSet.AttachmentSetRoutingInstances.Any())
            {
                if (attachmentSet.AttachmentRedundancy.Name != update.AttachmentRedundancy.ToString()) ValidationDictionary.AddError(string.Empty,
                    "The attachment redundancy option cannot be changed because routing instances are defined.");

                if (attachmentSet.SubRegion.Name != update.SubRegion) ValidationDictionary.AddError(string.Empty,
                    "The subregion cannot be changed because routing instances are defined.");
            }

            foreach (var vpnAttachmentSet in attachmentSet.VpnAttachmentSets)
            {
                var vpn = vpnAttachmentSet.Vpn;
                if (update.MulticastVpnDomainType == null)
                {
                    if (vpn.IsMulticastVpn)
                    {
                        ValidationDictionary.AddError(string.Empty, "A Multicast VPN Domain Type selection is required because the Attachment Set is "
                            + $"bound to Multicast VPN '{vpn.Name}'.");
                    }
                }

                if (update.MulticastVpnDomainType != null)
                {
                    if (vpn.IsMulticastVpn)
                    {
                        if (vpn.VpnTopologyType.TopologyType == SCM.Models.TopologyTypeEnum.HubandSpoke)
                        {
                            if (vpn.MulticastVpnDirectionType.MvpnDirectionType == MvpnDirectionTypeEnum.Unidirectional)
                            {
                                if (vpnAttachmentSet.IsHub.GetValueOrDefault())
                                {
                                    if (update.MulticastVpnDomainType != MulticastVpnDomainTypeEnum.SenderOnly)
                                    {
                                        ValidationDictionary.AddError(string.Empty, $"The Multicast VPN Domain Type selection of '{update.MulticastVpnDomainType.ToString()}' is not "
                                            + $"valid because Attachment Set '{attachmentSet.Name}' is designated as a HUB for hub-and-spoke multicast VPN "
                                            + $"'{vpn.Name}'. The Multicast Direction Type setting of the VPN is '{vpn.MulticastVpnDirectionType.Name}' and therefore "
                                            + "the Multicast VPN Domain Type of the Attachment Set must be 'Sender-Only'.");
                                    }
                                }
                                else
                                {
                                    if (update.MulticastVpnDomainType != MulticastVpnDomainTypeEnum.ReceiverOnly)
                                    {
                                        ValidationDictionary.AddError(string.Empty, $"The Multicast VPN Domain Type selection of '{update.MulticastVpnDomainType.ToString()}' is not "
                                            + $"valid because Attachment Set '{attachmentSet.Name}' is designated as a SPOKE for hub-and-spoke multicast VPN "
                                            + $"'{vpn.Name}'. The Multicast Direction Type setting of the VPN is '{vpn.MulticastVpnDirectionType.Name}' and therefore "
                                            + "the Multicast VPN Domain Type of the Attachment Set must be 'Receiver-Only'.");
                                    }
                                }
                            }
                            else
                            {
                                if (vpnAttachmentSet.IsHub.GetValueOrDefault())
                                {
                                    if (update.MulticastVpnDomainType != MulticastVpnDomainTypeEnum.SenderOnly
                                        && update.MulticastVpnDomainType != MulticastVpnDomainTypeEnum.SenderAndReceiver)
                                    {
                                        ValidationDictionary.AddError(string.Empty, $"The Multicast VPN Domain Type selection of '{update.MulticastVpnDomainType.ToString()}' is not "
                                            + $"valid because Attachment Set '{attachmentSet.Name}' is designated as a HUB for hub-and-spoke multicast VPN "
                                            + $"'{vpn.Name}'. The Multicast Direction Type setting of the VPN is '{vpn.MulticastVpnDirectionType.Name}' and therefore "
                                            + "the Multicast VPN Domain Type of the Attachment Set must be either 'Sender-and-Receiver' or 'Sender-Only'.");
                                    }
                                }
                                else
                                {
                                    if (update.MulticastVpnDomainType != MulticastVpnDomainTypeEnum.ReceiverOnly
                                        && update.MulticastVpnDomainType != MulticastVpnDomainTypeEnum.SenderAndReceiver)
                                    {
                                        ValidationDictionary.AddError(string.Empty, $"The Multicast VPN Domain Type selection of '{update.MulticastVpnDomainType.ToString()}' is not "
                                            + $"valid because Attachment Set '{attachmentSet.Name}' is designated as a SPOKE for hub-and-spoke multicast VPN "
                                            + $"'{vpn.Name}'. The Multicast Direction Type setting of the VPN is '{vpn.MulticastVpnDirectionType.Name}' and therefore "
                                            + "the Multicast VPN Domain Type of the Attachment Set must be either 'Sender-and-Receiver' or 'Receiver-Only'.");
                                    }
                                }
                            }
                        }

                        if (update.MulticastVpnDomainType == MulticastVpnDomainTypeEnum.ReceiverOnly)
                        {
                            var tenantMulticastGroups = (from groups in await _unitOfWork.TenantMulticastGroupRepository.GetAsync(q =>
                                                         q.VpnTenantMulticastGroups.Select(x => x.AttachmentSet).Any(x => x.AttachmentSetID == attachmentSetId))
                                                         select groups)
                                                         .ToList();
                            if (tenantMulticastGroups.Any())
                            {
                                ValidationDictionary.AddError(string.Empty, "The Multicast Domain Type cannot be 'Receiver-Only' for Attachment Set "
                                    + $"'{attachmentSet.Name}' because multicast group ranges are associated with the Attachment Set. "
                                    + "Remove the multicast group ranges from the Attachment Set first.");
                            }
                        }
                    }
                }
            }


            if (update.AttachmentRedundancy == AttachmentRedundancyEnum.Gold)
            {
                if (update.SubRegion == null)
                {
                    ValidationDictionary.AddError(string.Empty, "A subregion must be specified in order to use "
                        + $"gold attachment redundancy.");
                }
            }
        }
    }
}
