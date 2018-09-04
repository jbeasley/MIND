using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models.RequestModels;
using SCM.Services;
using SCM.Models;
using Mind.Services;
using SCM.Data;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for Attachment Set VRFs
    /// </summary>
    public class AttachmentSetRoutingInstanceValidator : BaseValidator, IAttachmentSetRoutingInstanceValidator
    {
        public AttachmentSetRoutingInstanceValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Validate a routing instance can be removed from an attachment set.
        /// </summary>
        /// <returns></returns>
        public async Task ValidateDeleteAsync(int attachmentSetRoutingInstanceId)
        {
            var attachmentSetRoutingInstance = (from attachmentSetRoutingInstances in await _unitOfWork.AttachmentSetRoutingInstanceRepository.GetAsync(q =>
                                                q.AttachmentSetRoutingInstanceID == attachmentSetRoutingInstanceId, 
                                                includeProperties: "AttachmentSet," +
                                                "RoutingInstance.BgpPeers.VpnTenantIpNetworksIn.TenantIpNetwork," +
                                                "RoutingInstance.BgpPeers.VpnTenantIpNetworksOut.TenantIpNetwork," +
                                                "RoutingInstance.VpnTenantIpNetworkRoutingInstances.TenantIpNetwork," +
                                                "RoutingInstance.BgpPeers.VpnTenantCommunitiesIn.TenantCommunity," +
                                                "RoutingInstance.BgpPeers.VpnTenantCommunitiesOut.TenantCommunity," +
                                                "RoutingInstance.VpnTenantCommunityRoutingInstances.TenantCommunity," +
                                                "RoutingInstance.VpnTenantIpNetworkStaticRouteRoutingInstances.TenantIpNetwork",
                                                AsTrackable: false)
                                                select attachmentSetRoutingInstances)
                                                .Single();

            var routingInstance = attachmentSetRoutingInstance.RoutingInstance;
            var attachmentSet = attachmentSetRoutingInstance.AttachmentSet;

            var vpnTenantNetworksIn = routingInstance.BgpPeers
                .SelectMany(x => x.VpnTenantIpNetworksIn
                .Where(y => y.AttachmentSet.AttachmentSetID == attachmentSet.AttachmentSetID));

            if (vpnTenantNetworksIn.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"Routing instance '{routingInstance.Name}' cannot be removed from attachment set "
                   + $"'{attachmentSet.Name}' because "
                   + $"it is used for inbound routing policy for the following tenant networks:");

                foreach (var vpnTenantNetwork in vpnTenantNetworksIn)
                {
                    ValidationDictionary.AddError(string.Empty, $"{vpnTenantNetwork.TenantIpNetwork.CidrName}");
                }
            }

            var vpnTenantNetworksOut = routingInstance.BgpPeers
                .SelectMany(x => x.VpnTenantIpNetworksOut
                .Where(y => y.AttachmentSetID == attachmentSet.AttachmentSetID));

            if (vpnTenantNetworksOut.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"Routing instance '{routingInstance.Name}' cannot be removed from attachment set "
                   + $"'{attachmentSet.Name}' because "
                   + $"it is used for outbound routing policy for the following tenant networks:");

                foreach (var vpnTenantNetwork in vpnTenantNetworksOut)
                {
                    ValidationDictionary.AddError(string.Empty, $"{vpnTenantNetwork.TenantIpNetwork.CidrName}.");
                }
            }

            var vpnTenantIpNetworkRoutingInstances = routingInstance.VpnTenantIpNetworkRoutingInstances
                .Where(x => x.AttachmentSetID == attachmentSet.AttachmentSetID);

            if (vpnTenantIpNetworkRoutingInstances.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"Routing instance '{routingInstance.Name}' cannot be removed from attachment set "
                   + $"'{attachmentSet.Name}' because "
                   + $"it is used for routing instance routing policy for the following tenant networks:");

                foreach (var vpnTenantIpNetwork in vpnTenantIpNetworkRoutingInstances)
                {
                    ValidationDictionary.AddError(string.Empty, $"{vpnTenantIpNetwork.TenantIpNetwork.CidrName}.");
                }
            }

            var vpnTenantIpNetworkStaticRouteRoutingInstances = routingInstance.VpnTenantIpNetworkStaticRouteRoutingInstances
                .Where(x => x.RoutingInstanceID == attachmentSetRoutingInstance.RoutingInstanceID);

            if (vpnTenantIpNetworkStaticRouteRoutingInstances.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"Routing instance '{routingInstance.Name}' cannot be removed from attachment set "
                   + $"'{attachmentSet.Name}' because "
                   + $"it is used for routing instance static routes for the following tenant networks:");

                foreach (var vpnTenantIpNetworkStaticRouteRoutingInstance in vpnTenantIpNetworkStaticRouteRoutingInstances)
                {
                    ValidationDictionary.AddError(string.Empty, $"{vpnTenantIpNetworkStaticRouteRoutingInstance.TenantIpNetwork.CidrName}.");
                }
            }

            var vpnTenantCommunitiesIn = routingInstance.BgpPeers
                .SelectMany(x => x.VpnTenantCommunitiesIn
                .Where(y => y.AttachmentSetID == attachmentSet.AttachmentSetID));

            if (vpnTenantCommunitiesIn.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"Routing instance '{routingInstance.Name}' cannot be removed from attachment set "
                   + $"'{attachmentSet.Name}' because "
                   + $"it is used for inbound routing policy for the following tenant communities:");

                foreach (var vpnTenantCommunity in vpnTenantCommunitiesIn)
                {
                    ValidationDictionary.AddError(string.Empty, $"{vpnTenantCommunity.TenantCommunity.Name}.");
                }
            }

            var vpnTenantCommunitiesOut = routingInstance.BgpPeers
                .SelectMany(x => x.VpnTenantCommunitiesOut
                .Where(y => y.AttachmentSetID == attachmentSet.AttachmentSetID));

            if (vpnTenantCommunitiesOut.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"Routing instance '{routingInstance.Name}' cannot be removed from attachment set "
                   + $"'{attachmentSet.Name}' because "
                   + $"it is used for outbound routing policy for the following tenant communities:");

                foreach (var vpnTenantCommunity in vpnTenantCommunitiesOut)
                {
                    ValidationDictionary.AddError(string.Empty, $"{vpnTenantCommunity.TenantCommunity.Name}.");
                }
            }

            var vpnTenantCommunityRoutingInstances = routingInstance.VpnTenantCommunityRoutingInstances
                .Where(x => x.AttachmentSetID == attachmentSet.AttachmentSetID);

            if (vpnTenantCommunityRoutingInstances.Any())
            {
                ValidationDictionary.AddError(string.Empty, $"Routing instance '{routingInstance.Name}' cannot be removed from attachment set "
                   + $"'{attachmentSet.Name}' because "
                   + $"it is used for routing instance routing policy for the following tenant communities:");

                foreach (var vpnTenantCommunityRoutingInstance in vpnTenantCommunityRoutingInstances)
                {
                    ValidationDictionary.AddError(string.Empty, $"{vpnTenantCommunityRoutingInstance.TenantCommunity.Name}.");
                }
            }
        }

        /// <summary>
        /// Validate the routing instances associated with an attachment set for a given VPN are configured correctly.
        /// </summary>
        /// <param name="vpn"></param>
        /// <returns></returns>
        public async Task ValidateRoutingInstancesForVpnAsync(int vpnId)
        {
            var attachmentSetIds = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(q => 
                                  q.VpnAttachmentSets.Where(x => x.VpnID == vpnId).Select(x => x.AttachmentSet).Any())
                                  select result.AttachmentSetID)
                                  .ToList();

            await Task.WhenAll(from attachmentSetId in attachmentSetIds
                               select ValidateRoutingInstancesForAttachmentSetAsync(attachmentSetId));
        }

        /// <summary>
        /// Validate that the routing instances for an attachment set are configured correctly in accordance with the 
        /// required attachment redundancy (e.g. Bronze, Silver, Gold, Custom) of the attachment set.
        /// </summary>
        /// <returns></returns>
        public async Task ValidateRoutingInstancesForAttachmentSetAsync(int attachmentSetId)
        {
            var attachmentSet= (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(q =>
                                                q.AttachmentSetID == attachmentSetId, 
                                                includeProperties: "AttachmentSet.AttachmentRedundancy.AttachmentRedundancyType," +
                                                "AttachmentSet.Tenant," +
                                                "AttachmentSetRoutingInstance.RoutingInstance.Attachments," +
                                                "AttachmentSetRoutingInstance.RoutingInstance.Vifs," +
                                                "AttachmentSetRoutingInstance.RoutingInstance.Device.Location.SubRegion," +
                                                "AttachmentSetRoutingInstance.RoutingInstance.Device.Plane",
                                                AsTrackable: false)
                                                select result)
                                                .Single();

            var attachmentRedundancy = attachmentSet.AttachmentRedundancy;
            var attachmentSetRoutingInstances = attachmentSet.AttachmentSetRoutingInstances.ToList();
            if (attachmentRedundancy.AttachmentRedundancyType != AttachmentRedundancyTypeEnum.Custom)
            {
                foreach (var attachmentSetRoutingInstance in attachmentSetRoutingInstances)
                {
                    var routingInstance = attachmentSetRoutingInstance.RoutingInstance;
                    if (routingInstance.Attachments.Count() + routingInstance.Vifs.Count() > 1)
                    {
                        ValidationDictionary.AddError(string.Empty, $"Routing instance '{routingInstance.Name}' is associated with multiple logical attachments. "
                            + "The routing instance can be associated with at most one logical attachment. "
                            + "Use a custom attachment Set if you wish to associate the routing instance with more than one logical attachment.");
                    }
                }
            }

            if (attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Bronze)
            {
                if (attachmentSetRoutingInstances.Count != 1)
                {
                    ValidationDictionary.AddError(string.Empty, $"One, and no more than one, routing instance for bronze attachment set "
                        + $"'{attachmentSet.Name}' belonging to tenant '{attachmentSet.Tenant.Name}' must be defined.");
                }
            }
            else
            {
                if (attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Silver)
                {
                    if (attachmentSetRoutingInstances.Count != 2)
                    {
                        ValidationDictionary.AddError(string.Empty, "Two, and no more than two, routing instances for silver attachment set "
                            + $"'{attachmentSet.Name}' "
                            + $"belonging to tenant '{attachmentSet.Tenant.Name}' must be defined. "
                            + "Each routing instance must belong to a device in the same location.");
                    }
                    else
                    {
                        var locationA = attachmentSetRoutingInstances[0].RoutingInstance.Device.Location;
                        var locationB = attachmentSetRoutingInstances[1].RoutingInstance.Device.Location;
                        var planeA = attachmentSetRoutingInstances[0].RoutingInstance.Device.Plane;
                        var planeB = attachmentSetRoutingInstances[1].RoutingInstance.Device.Plane;

                        if (locationA.LocationID != locationB.LocationID)
                        {
                            ValidationDictionary.AddError(string.Empty, $"The location for each routing instance in silver attachment set '{attachmentSet.Name}' "
                                + $"belonging to tenant '{attachmentSet.Tenant.Name}' must be the same.");
                        }

                        if (planeA.PlaneID == planeB.PlaneID)
                        {
                            ValidationDictionary.AddError(string.Empty, $"The plane for each routing instance in silver attachment set '{attachmentSet.Name}' "
                                + $"belonging to tenant '{attachmentSet.Tenant.Name}'must be different.");
                        }
                    }
                }

                else if (attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Gold)
                {
                    if (attachmentSetRoutingInstances.Count != 2)
                    {
                        ValidationDictionary.AddError(string.Empty, $"Two, and no more than two, routing instances for gold attachment set '{attachmentSet.Name}' "
                            + $"belonging to tenant '{attachmentSet.Tenant.Name}' must be defined. "
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
                            ValidationDictionary.AddError(string.Empty, $"The sub-region for each routing instance in gold attachment set '{attachmentSet.Name}' "
                                + $"belonging to tenant '{attachmentSet.Tenant.Name}' must be the same.");
                        }

                        if (locationA.LocationID == locationB.LocationID)
                        {
                            ValidationDictionary.AddError(string.Empty, $"The location for each routing instance in gold attachment set '{attachmentSet.Name}' "
                                + $"belonging to tenant '{attachmentSet.Tenant.Name}' must be different.");
                        }

                        if (planeA.PlaneID == planeB.PlaneID)
                        {
                            ValidationDictionary.AddError(string.Empty, $"The Plane for each VRF in gold Attachment Set '{attachmentSet.Name}' "
                                + $"belonging to Tenant '{attachmentSet.Tenant.Name}' must be different.");
                        }
                    }
                }
                else if (attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Custom)
                {
                    if (!attachmentSetRoutingInstances.Any())
                    {
                        ValidationDictionary.AddError(string.Empty, $"At least one routing instance is required for custom attachment set '{attachmentSet.Name}' "
                            + $"belonging to tenant '{attachmentSet.Tenant.Name}'.");
                    }
                }
            }
        }
    }
}
