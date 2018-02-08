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
    /// Validator for Attachment Set VRFs
    /// </summary>
    public class AttachmentSetRoutingInstanceValidator : BaseValidator, IAttachmentSetRoutingInstanceValidator
    {
        public AttachmentSetRoutingInstanceValidator(IRoutingInstanceService routingInstanceService,
            IAttachmentSetService attachmentSetService,
            IAttachmentSetRoutingInstanceService attachmentSetRoutingInstanceService)
        {
            RoutingInstanceService = routingInstanceService;
            AttachmentSetService = attachmentSetService;
            AttachmentSetRoutingInstanceService = attachmentSetRoutingInstanceService;
        }

        private IRoutingInstanceService RoutingInstanceService { get; set; }
        private IAttachmentSetService AttachmentSetService { get; set; }
        private IAttachmentSetRoutingInstanceService AttachmentSetRoutingInstanceService { get; set; }

        /// <summary>
        /// Validate a new Attachment Set VRF
        /// </summary>
        /// <param name="attachmentSetRoutingInstance"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(AttachmentSetRoutingInstance attachmentSetRoutingInstance)
        {
            var routingInstance = await RoutingInstanceService.GetByIDAsync(attachmentSetRoutingInstance.RoutingInstanceID);
            var attachmentSet = await AttachmentSetService.GetByIDAsync(attachmentSetRoutingInstance.AttachmentSetID);

            if (attachmentSet.IsLayer3 != routingInstance.RoutingInstanceType.IsLayer3)
            {
                ValidationDictionary.AddError(string.Empty, $"VRF '{routingInstance.Name}' cannot be added to Attachment Set '{attachmentSet.Name}'. "
                + "The protocol layer of the Attachment Set and the VRF do not match. "
                + $"Attachment Set 'IsLayer3' property is '{attachmentSet.IsLayer3}'. "
                + $"VRF 'IsLayer3' property is '{routingInstance.RoutingInstanceType.IsLayer3}'.");
            }

            // The VRF must belong to the specified Tenant

            if (routingInstance.Tenant.TenantID != attachmentSet.TenantID)
            {
                ValidationDictionary.AddError(string.Empty, $"VRF '{routingInstance.Name}' "
                   + $" does not belong to Tenant {attachmentSet.Tenant.Name}.");
            }

            // The VRF must be associated with a Device in the specified Region

            if (routingInstance.Device.Location.SubRegion.Region.RegionID != attachmentSet.RegionID)
            {
                ValidationDictionary.AddError(string.Empty, $"VRF '{routingInstance.Name}' is not associated with "
                     + $"a Device in Region {attachmentSet.Region.Name}.");
            }
        }

        /// <summary>
        /// Validate a VRF can be removed from an Attachment Set.
        /// </summary>
        /// <param name="attachmentSetRoutingInstance"></param>
        /// <returns></returns>
        public async Task ValidateDeleteAsync(AttachmentSetRoutingInstance attachmentSetRoutingInstance)
        {
            var routingInstance = await RoutingInstanceService.GetByIDAsync(attachmentSetRoutingInstance.RoutingInstanceID);

            var vpnTenantNetworksIn = routingInstance.BgpPeers
                .SelectMany(x => x.VpnTenantNetworksIn
                .Where(y => y.AttachmentSet.AttachmentSetID == attachmentSetRoutingInstance.AttachmentSetID));

            if (vpnTenantNetworksIn.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"VRF '{routingInstance.Name}' cannot be removed from Attachment Set "
                   + $"'{attachmentSetRoutingInstance.AttachmentSet.Name}' because "
                   + $"it is used for inbound routing policy for the following Tenant Networks:");

                foreach (var vpnTenantNetwork in vpnTenantNetworksIn)
                {
                    ValidationDictionary.AddError(string.Empty, $"{vpnTenantNetwork.TenantNetwork.CidrName}");
                }
            }

            var vpnTenantNetworksOut = routingInstance.BgpPeers
                .SelectMany(x => x.VpnTenantNetworksOut
                .Where(y => y.AttachmentSetID == attachmentSetRoutingInstance.AttachmentSetID));

            if (vpnTenantNetworksOut.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"VRF '{routingInstance.Name}' cannot be removed from Attachment Set "
                   + $"'{attachmentSetRoutingInstance.AttachmentSet.Name}' because "
                   + $"it is used for outbound routing policy for the following Tenant Networks:");

                foreach (var vpnTenantNetwork in vpnTenantNetworksOut)
                {
                    ValidationDictionary.AddError(string.Empty, $"{vpnTenantNetwork.TenantNetwork.CidrName}.");
                }
            }

            var vpnTenantNetworksRoutingInstance = routingInstance.VpnTenantNetworksRoutingInstance
                .Where(x => x.AttachmentSetID == attachmentSetRoutingInstance.AttachmentSetID);

            if (vpnTenantNetworksRoutingInstance.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"VRF '{routingInstance.Name}' cannot be removed from Attachment Set "
                   + $"'{attachmentSetRoutingInstance.AttachmentSet.Name}' because "
                   + $"it is used for VRF routing policy for the following Tenant Networks:");

                foreach (var vpnTenantNetwork in vpnTenantNetworksRoutingInstance)
                {
                    ValidationDictionary.AddError(string.Empty, $"{vpnTenantNetwork.TenantNetwork.CidrName}.");
                }
            }

            var vpnTenantNetworkStaticRoutesRoutingInstance = routingInstance.VpnTenantNetworkStaticRoutesRoutingInstance
                .Where(x => x.RoutingInstanceID == attachmentSetRoutingInstance.RoutingInstanceID);

            if (vpnTenantNetworkStaticRoutesRoutingInstance.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"VRF '{routingInstance.Name}' cannot be removed from Attachment Set "
                   + $"'{attachmentSetRoutingInstance.AttachmentSet.Name}' because "
                   + $"it is used for VRF static routes for the following Tenant Networks:");

                foreach (var vpnTenantNetworkStaticRouteRoutingInstance in vpnTenantNetworkStaticRoutesRoutingInstance)
                {
                    ValidationDictionary.AddError(string.Empty, $"{vpnTenantNetworkStaticRouteRoutingInstance.TenantNetwork.CidrName}.");
                }
            }

            var vpnTenantCommunitiesIn = routingInstance.BgpPeers
                .SelectMany(x => x.VpnTenantCommunitiesIn
                .Where(y => y.AttachmentSetID == attachmentSetRoutingInstance.AttachmentSetID));

            if (vpnTenantCommunitiesIn.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"VRF '{routingInstance.Name}' cannot be removed from Attachment Set "
                   + $"'{attachmentSetRoutingInstance.AttachmentSet.Name}' because "
                   + $"it is used for inbound routing policy for the following Tenant Communities:");

                foreach (var vpnTenantCommunity in vpnTenantCommunitiesIn)
                {
                    ValidationDictionary.AddError(string.Empty, $"{vpnTenantCommunity.TenantCommunity.Name}.");
                }
            }

            var vpnTenantCommunitiesOut = routingInstance.BgpPeers
                .SelectMany(x => x.VpnTenantCommunitiesOut
                .Where(y => y.AttachmentSetID == attachmentSetRoutingInstance.AttachmentSetID));

            if (vpnTenantCommunitiesOut.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"VRF '{routingInstance.Name}' cannot be removed from Attachment Set "
                   + $"'{attachmentSetRoutingInstance.AttachmentSet.Name}' because "
                   + $"it is used for outbound routing policy for the following Tenant Communities:");

                foreach (var vpnTenantCommunity in vpnTenantCommunitiesOut)
                {
                    ValidationDictionary.AddError(string.Empty, $"{vpnTenantCommunity.TenantCommunity.Name}.");
                }
            }

            var vpnTenantCommunitiesRoutingInstance = routingInstance.VpnTenantCommunitiesRoutingInstance
                .Where(x => x.AttachmentSetID == attachmentSetRoutingInstance.AttachmentSetID);

            if (vpnTenantCommunitiesRoutingInstance.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"VRF '{routingInstance.Name}' cannot be removed from Attachment Set "
                   + $"'{attachmentSetRoutingInstance.AttachmentSet.Name}' because "
                   + $"it is used for VRF routing policy for the following Tenant Communities:");

                foreach (var vpnTenantCommunity in vpnTenantCommunitiesRoutingInstance)
                {
                    ValidationDictionary.AddError(string.Empty, $"{vpnTenantCommunity.TenantCommunity.Name}.");
                }
            }
        }

        /// <summary>
        /// Validate all Attachment Set VRFs for a given VPN.
        /// </summary>
        /// <param name="vpn"></param>
        /// <returns></returns>
        public async Task ValidateRoutingInstancesConfiguredCorrectlyAsync(Vpn vpn)
        {
            var tasks = new List<Task>();
            var attachmentSets = await AttachmentSetService.GetAllByVpnIDAsync(vpn.VpnID);

            foreach (var attachmentSet in attachmentSets)
            {
                tasks.Add(ValidateRoutingInstancesConfiguredCorrectlyAsync(attachmentSet));
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Validate that the VRFs for an Attachment Set are correct in accordance with the 
        /// defined Attachment Redundancy (e.g. Bronze, Silver, Gold, Custom).
        /// </summary>
        /// <param name="attachmentSet"></param>
        /// <returns></returns>
        public async Task ValidateRoutingInstancesConfiguredCorrectlyAsync(AttachmentSet attachmentSet)
        {
            var v = await AttachmentSetRoutingInstanceService.GetAllByAttachmentSetIDAsync(attachmentSet.AttachmentSetID);
            var attachmentSetRoutingInstances = v.ToList();

            var attachmentRedundancy = attachmentSet.AttachmentRedundancy;
            if (attachmentRedundancy.AttachmentRedundancyType != AttachmentRedundancyType.Custom) {
                foreach (var x in attachmentSetRoutingInstances)
                {
                    var numOfAttachments = x.RoutingInstance.Attachments.Count() + x.RoutingInstance.Vifs.Count();
                    if (numOfAttachments > 1)
                    {
                        ValidationDictionary.AddError(string.Empty, $"VRF '{x.RoutingInstance.Name}' is associated with multiple Logical Attachments. "
                            + "The VRF can be associated with at most one Logical Attachment. "
                            + "Use a Custom Attachment Set if you wish to associate the VRF with more than one Logical Attachment.");
                    }
                }
            }

            if (attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyType.Bronze)
            {
                if (attachmentSetRoutingInstances.Count != 1)
                {
                    ValidationDictionary.AddError(string.Empty, $"One, and no more than one, VRF for bronze Attachment Set "
                        + $"'{attachmentSet.Name}' belonging to Tenant '{attachmentSet.Tenant.Name}' must be defined.");
                }
            }
            else
            {
                if (attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyType.Silver)
                {
                    if (attachmentSetRoutingInstances.Count != 2)
                    {
                        ValidationDictionary.AddError(string.Empty, "Two, and no more than two, VRFs for silver Attachment Set "
                            + $"'{attachmentSet.Name}' "
                            + $"belonging to tenant '{attachmentSet.Tenant.Name}' must be defined. "
                            + "Each VRF must be in the same Location.");
                    }
                    else
                    {
                        var locationA = attachmentSetRoutingInstances[0].RoutingInstance.Device.Location;
                        var locationB = attachmentSetRoutingInstances[1].RoutingInstance.Device.Location;
                        var planeA = attachmentSetRoutingInstances[0].RoutingInstance.Device.Plane;
                        var planeB = attachmentSetRoutingInstances[1].RoutingInstance.Device.Plane;

                        if (locationA.LocationID != locationB.LocationID)
                        {
                            ValidationDictionary.AddError(string.Empty, $"The Location for each VRF in silver Attachment Set '{attachmentSet.Name}' "
                                + $"belonging to Tenant '{attachmentSet.Tenant.Name}' must be the same.");
                        }

                        if (planeA.PlaneID == planeB.PlaneID)
                        {
                            ValidationDictionary.AddError(string.Empty, $"The plane for each VRF in silver Attachment Set '{attachmentSet.Name}' "
                                + $"belonging to Tenant '{attachmentSet.Tenant.Name}'must be different.");
                        }
                    }
                }

                else if (attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyType.Gold)
                {
                    if (attachmentSetRoutingInstances.Count != 2)
                    {
                        ValidationDictionary.AddError(string.Empty, $"Two, and no more than two, VRFs for gold Attachment Set '{attachmentSet.Name}' "
                            + $"belonging to Tenant '{attachmentSet.Tenant.Name}' must be defined. "
                            + "Each VRF must be in a different Location.");
                    }
                    else
                    {

                        var locationA = attachmentSetRoutingInstances[0].RoutingInstance.Device.Location;
                        var locationB = attachmentSetRoutingInstances[1].RoutingInstance.Device.Location;
                        var planeA = attachmentSetRoutingInstances[0].RoutingInstance.Device.Plane;
                        var planeB = attachmentSetRoutingInstances[1].RoutingInstance.Device.Plane;
                        var subRegionA = locationA.SubRegion;
                        var subRegionB = locationB.SubRegion;

                        if (subRegionA.SubRegionID != subRegionB.SubRegionID)
                        {
                            ValidationDictionary.AddError(string.Empty, $"The sub-region for each VRF in gold Attachment Set '{attachmentSet.Name}' "
                                + $"belonging to Tenant '{attachmentSet.Tenant.Name}' must be the same.");
                        }

                        if (locationA.LocationID == locationB.LocationID)
                        {
                            ValidationDictionary.AddError(string.Empty, $"The Location for each VRF in gold Attachment Set '{attachmentSet.Name}' "
                                + $"belonging to Tenant '{attachmentSet.Tenant.Name}' must be different.");
                        }

                        if (planeA.PlaneID == planeB.PlaneID)
                        {
                            ValidationDictionary.AddError(string.Empty, $"The Plane for each VRF in gold Attachment Set '{attachmentSet.Name}' "
                                + $"belonging to Tenant '{attachmentSet.Tenant.Name}' must be different.");
                        }
                    }
                }
                else if (attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyType.Custom)
                {
                    if (attachmentSetRoutingInstances.Count == 0)
                    {
                        ValidationDictionary.AddError(string.Empty, $"At least one VRF is required for custom Attachment Set '{attachmentSet.Name}' "
                            + $"belonging to Tenant '{attachmentSet.Tenant.Name}'.");
                    }
                }
            }
        }
    }
}
