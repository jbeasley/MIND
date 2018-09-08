using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using SCM.Models;
using Mind.Models.RequestModels;
using SCM.Data;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for vpns
    /// </summary>
    public class VpnValidator : BaseValidator, IVpnValidator
    {
        public VpnValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Validate changes to a vpn
        /// </summary>
        /// <param name="vpnId"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(int vpnId, VpnUpdate update)
        {
            var vpn = (from result in await _unitOfWork.VpnRepository.GetAsync(
                    x =>
                        x.VpnID == vpnId,
                        includeProperties: "VpnAttachmentSets.AttachmentSet.Tenant," +
                        "VpnAttachmentSets.AttachmentSet.AttachmentSetRoutingInstances.RoutingInstance.Device.Location.SubRegion.Region," +
                        "VpnTenancyType," +
                        "VpnTopologyType," +
                        "ExtranetVpns," +
                        "ExtranetVpnMembers," +
                        "MulticastVpnDirectionType")
                        select result)
                        .Single();

            if (update.TenancyType == Mind.Models.RequestModels.TenancyTypeEnum.Single && vpn.VpnTenancyType.TenancyType == SCM.Models.TenancyTypeEnum.Multi)
            {
                // The tenancy type can be narrowed ('Multi' to 'Single') if the only Tenant 
                // of the VPN is the 'Owner'.
                var tenants = vpn.VpnAttachmentSets.Select(s => s.AttachmentSet.Tenant);
                var ownerCount = tenants.Count(s => s.Name == vpn.Tenant.Name);

                if (ownerCount != tenants.Count())
                {
                    ValidationDictionary.AddError(string.Empty, "The tenancy type cannot be changed from Multi to Single because tenants other "
                        + "than the owner are attached to the VPN.");
                }
            }

            // Check if region has been narrowed from no region setting (the vpn can be operate globally), 
            // to a specific region (the vpn can operate only in the specified region), or changed from one region to another
            if (update.Region.HasValue && (vpn.RegionID == null || update.Region.ToString() != vpn.Region.Name))
            {
                // Region can be narrowed or changed only if the only devices with routing instances which participate in the vpn are in the 
                // specified region.

                var distinctRegions = vpn.VpnAttachmentSets.SelectMany(
                        vpnAttachmentSet =>
                            vpnAttachmentSet.AttachmentSet.AttachmentSetRoutingInstances
                            .Select(
                                attachmentSetRoutingInstance => 
                                    attachmentSetRoutingInstance.RoutingInstance.Device.Location.SubRegion.Region))
                            .GroupBy(q => q.RegionID)
                            .Select(group => group.First());

                if (distinctRegions.Count() == 1)
                {
                    var region = distinctRegions.Single();
                    if (region.RegionID != vpn.RegionID)
                    {
                        ValidationDictionary.AddError(string.Empty, "The region setting cannot be narrowed to a specific region because all of the tenants "
                            + "of the vpn exist in the " + region.Name + " region.");
                    }
                }
                else if (distinctRegions.Count() > 1)
                {
                    ValidationDictionary.AddError(string.Empty, "The region setting cannot be narrowed to a specific region because tenants of the VPN "
                        + "exist in more than one region.");
                }
            }

            if (vpn.IsExtranet)
            {
                if (update.Region.HasValue && update.Region.ToString() != vpn.Region.Name)
                {
                    if (vpn.ExtranetVpnMembers.Any())
                    {
                        ValidationDictionary.AddError(string.Empty, $"The region of vpn '{vpn.Name}' cannot be changed because the vpn is an extranet "
                            + "and member vpns of the extranet are defined. Remove the member vpns first.");
                    }
                }
            }

            // Extranet vpn logic follows...
            if (update.IsExtranet.HasValue)
            {
                if (update.IsExtranet.Value)
                {
                    if (vpn.ExtranetVpns.Any())
                    {
                        ValidationDictionary.AddError(string.Empty, $"Vpn '{vpn.Name}' cannot be an extranet "
                            + "because the vpn is a member of at least one other extranet vpn.");
                    }
                }
                else
                {
                    if (vpn.ExtranetVpnMembers.Any())
                    {
                        ValidationDictionary.AddError(string.Empty, $"Vpn '{vpn.Name}' must be enabled for extranet because extranet vpn members are defined. "
                            + "Remove the member vpns from the extranet vpn first.");
                    }

                    if (update.Region.HasValue && update.Region.ToString() != vpn.Region.Name)
                    {
                        if (vpn.ExtranetVpns.Any())
                        {
                            ValidationDictionary.AddError(string.Empty, $"The region of vpn '{vpn.Name}' cannot be changed because the vpn is a member of "
                                + "at least one extranet vpn. Remove the vpn from the extranet first.");
                        }
                    }
                }
            }

            // Multicast vpn checks...
            if (vpn.IsMulticastVpn)
            {
                if (vpn.VpnTopologyType.TopologyType == SCM.Models.TopologyTypeEnum.HubandSpoke)
                {
                    if (!update.MulticastVpnDirectionType.HasValue)
                    {
                        ValidationDictionary.AddError(string.Empty, "A multicast vpn direction type option must be specified because " +
                            "the vpn topology is hub-and-spoke.");
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Validate deletion of a VPN.
        /// </summary>
        /// <param name="vpnId"></param>
        public async Task ValidateDeleteAsync(int vpnId)
        {
            var vpn = (from vpns in await _unitOfWork.VpnRepository.GetAsync(
                x =>
                    x.VpnID == vpnId,
                    includeProperties: "ExtranetVpnMembers",
                    AsTrackable: false)
                       select vpns)
                       .Single();

            if (vpn.IsExtranet && vpn.ExtranetVpnMembers.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"Extranet vpn '{vpn.Name}' cannot be deleted because member vpns are defined. "
                    + "Remove the member vpns from the extranet first.");
            }
        }
    }
}
