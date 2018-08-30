using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;
using SCM.Models;
using SCM.Models.RequestModels;

namespace SCM.Validators
{
    /// <summary>
    /// Validator for VPNs
    /// </summary>
    public class VpnValidator : BaseValidator, IVpnValidator
    {
        public VpnValidator(IVpnService vpnService,
            IVpnTopologyTypeService vpnTopologyTypeService,
            IVpnTenancyTypeService vpnTenancyTypeService,
            IVpnTenantIpNetworkInService vpnTenantNetworkInService,
            IVpnTenantCommunityInService vpnTenantCommunityInService,
            IVpnTenantMulticastGroupService vpnTenantMulticastGroupService,
            IMulticastVpnDirectionTypeService multicastVpnDirectionTypeService,
            IRouteTargetRangeService routeTargetRangeService)
        {
            VpnService = vpnService;
            VpnTopologyTypeService = vpnTopologyTypeService;
            VpnTenancyTypeService = vpnTenancyTypeService;
            VpnTenantNetworkInService = vpnTenantNetworkInService;
            VpnTenantCommunityInService = vpnTenantCommunityInService;
            VpnTenantMulticastGroupService = vpnTenantMulticastGroupService;
            MulticastVpnDirectionTypeService = multicastVpnDirectionTypeService;
            RouteTargetRangeService = routeTargetRangeService;
        }

        private IVpnService VpnService { get; }
        private IVpnTopologyTypeService VpnTopologyTypeService { get; }
        private IVpnTenancyTypeService VpnTenancyTypeService { get; }
        private IVpnTenantIpNetworkInService VpnTenantNetworkInService { get; }
        private IVpnTenantCommunityInService VpnTenantCommunityInService { get; }
        private IVpnTenantMulticastGroupService VpnTenantMulticastGroupService { get; }
        private IMulticastVpnDirectionTypeService MulticastVpnDirectionTypeService { get; }
        private IRouteTargetRangeService RouteTargetRangeService { get; }

        /// <summary>
        /// Validate a new VPN
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task ValidateNewAsync(VpnRequest request)
        {
            var topologyType = await VpnTopologyTypeService.GetByIDAsync(request.VpnTopologyTypeID);
            if (topologyType.VpnProtocolType.ProtocolType == ProtocolTypeEnum.IP)
            {
                if (request.AddressFamilyID == null)
                {
                    ValidationDictionary.AddError(string.Empty, "An Address Family option must be selected.");
                }

                // A Route Target Range MUST be specfied for non-Nova VPN requests

                if (!request.IsNovaVpn)
                {
                    if (request.RouteTargetRangeID == null)
                    {
                        ValidationDictionary.AddError(string.Empty, "A Route Target Range option must be selected.");
                    }
                    else
                    {
                        var routeTargetRange = await RouteTargetRangeService.GetByIDAsync(request.RouteTargetRangeID.Value);
                        if (routeTargetRange.Name == "Default")
                        {
                            ValidationDictionary.AddError(string.Empty, "The default Route Target Range cannot be used for non-Nova VPNs. "
                                + "Select another Route Target Range.");
                        }
                    }
                }
            }

            var tenancyType = await VpnTenancyTypeService.GetByIDAsync(request.VpnTenancyTypeID);
            if (request.IsExtranet)
            {
                if (tenancyType.TenancyType != TenancyTypeEnum.Multi)
                {
                    ValidationDictionary.AddError(string.Empty, "The Tenancy Type must be 'Multi' for Extranet VPNs.");
                }
            }

            if (request.IsMulticastVpn)
            {
                if (request.PlaneID == null)
                {
                    ValidationDictionary.AddError(string.Empty, "A Plane must be selected for Multicast VPNs.");
                }

                if (request.IsExtranet)
                {
                    ValidationDictionary.AddError(string.Empty, "Extranet is not currently available for Multicast VPNs.");
                }

                if (request.MulticastVpnServiceTypeID == null)
                {
                    ValidationDictionary.AddError(string.Empty, "A Multicast VPN Service Type must be specified.");
                }

                if (topologyType.TopologyType == TopologyTypeEnum.HubandSpoke)
                {
                    if (request.MulticastVpnDirectionTypeID == null)
                    {
                        ValidationDictionary.AddError(string.Empty, "A Multicast VPN Direction Type must be specified for a Hub-and-Spoke VPN.");
                    }
                }
            }
        }

        /// <summary>
        /// Validate changes to a VPN
        /// </summary>
        /// <param name="vpn"></param>
        /// <returns></returns>
        public async Task ValidateChangesAsync(Vpn vpn)
        {
            var currentVpn = await VpnService.GetByIDAsync(vpn.VpnID);
            var vpnTenancyType = await VpnTenancyTypeService.GetByIDAsync(vpn.VpnTenancyTypeID);

            if (vpnTenancyType.TenancyType == TenancyTypeEnum.Single && currentVpn.VpnTenancyType.TenancyType == TenancyTypeEnum.Multi)
            {
                // The tenancy type can be narrowed ('Multi' to 'Single') if the only Tenant 
                // of the VPN is the 'Owner'.

                var tenants = currentVpn.VpnAttachmentSets.Select(s => s.AttachmentSet.Tenant);
                var ownerCount = tenants.Count(s => s.Name == currentVpn.Tenant.Name);

                if (ownerCount != tenants.Count())
                {
                    ValidationDictionary.AddError(string.Empty, "The Tenancy Type cannot be changed from Multi to Single because Tenants other "
                        + "than the owner are attached to the VPN.");
                }
            }

            // Check if Region has been narrowed from no Region setting which means the VPN can be deployed anywhere globally, 
            // to a specific Region which means the VPN can be deployed only to the specified Region, or changed from one Region to another

            if (vpn.RegionID != null && (currentVpn.RegionID == null || vpn.RegionID != currentVpn.RegionID))
            {
                // Region can be narrowed or changed only if the only Devices with VRFs which participate in the VPN are in the 
                // selected Region.

                var regions = currentVpn.VpnAttachmentSets.SelectMany(q =>
                q.AttachmentSet.AttachmentSetRoutingInstances.Select(r => r.RoutingInstance.Device.Location.SubRegion.Region));

                var distinctRegions = regions.GroupBy(q => q.RegionID).Select(group => group.First());
                if (distinctRegions.Count() == 1)
                {
                    var region = distinctRegions.Single();
                    if (region.RegionID != vpn.RegionID)
                    {
                        ValidationDictionary.AddError(string.Empty, "The Region setting cannot be narrowed to a specific Region because all of the Tenants "
                            + "of the VPN exist in the " + region.Name + " Region.");
                    }
                }
                else if (distinctRegions.Count() > 1)
                {
                    ValidationDictionary.AddError(string.Empty, "The Region setting cannot be narrowed to a specific Region because Tenants of the VPN "
                        + "exist in more than one Region.");
                }
            }

            if (vpn.IsExtranet)
            {
                // Extranet VPN Tenancy Type must be Multi

                if (vpnTenancyType.TenancyType != TenancyTypeEnum.Multi)
                {
                    ValidationDictionary.AddError(string.Empty, "The Extranet attribute can only be set for Multi-Tenant VPNs.");
                }

                if (vpn.RegionID != currentVpn.RegionID)
                {
                    if (currentVpn.ExtranetVpnMembers.Any())
                    {
                        ValidationDictionary.AddError(string.Empty, $"The region of VPN '{vpn.Name}' cannot be changed because the VPN is an Extranet "
                            + "and member VPNs of the Extranet are defined. Remove the member VPNs first.");
                    }
                }

                if (currentVpn.ExtranetVpns.Any())
                {
                    ValidationDictionary.AddError(string.Empty, $"VPN '{vpn.Name}' cannot be an Extranet "
                        + "because the VPN is a member VPN of at least one other Extranet VPN.");
                }

                if (currentVpn.IsMulticastVpn)
                {
                    ValidationDictionary.AddError(string.Empty, $"VPN '{vpn.Name}' cannot be an Extranet "
                        + "because Extranet is not currently supported for Multicast VPN.");
                }
            }
            else
            {
                if (currentVpn.ExtranetVpnMembers.Any())
                {
                    ValidationDictionary.AddError(string.Empty, $"VPN '{vpn.Name}' must be enabled for Extranet because Extranet VPN Members are defined. "
                        + "Remove the member VPNs from the Extranet VPN first and then disable the Extranet attribute.");
                }

                if (vpn.RegionID != currentVpn.RegionID)
                {
                    if (currentVpn.ExtranetVpns.Any())
                    {
                        ValidationDictionary.AddError(string.Empty, $"The region of VPN '{vpn.Name}' cannot be changed because the VPN is a member of "
                            + "at least one Extranet VPN. Remove the VPN from the Extranet first.");
                    }
                }
            }

            if (currentVpn.IsMulticastVpn)
            {
                var vpnTopologyType = await VpnTopologyTypeService.GetByIDAsync(vpn.VpnTopologyTypeID);
                if (vpnTopologyType.TopologyType == TopologyTypeEnum.HubandSpoke)
                {
                    if (vpn.MulticastVpnDirectionTypeID == null)
                    {
                        ValidationDictionary.AddError(string.Empty, "A Multicast VPN Direction Type must be specified for a Hub-and-Spoke VPN.");
                        return;
                    }
                
                    var multicastVpnDirectionType = await MulticastVpnDirectionTypeService.GetByIDAsync(vpn.MulticastVpnDirectionTypeID.Value);
                    if (currentVpn.MulticastVpnDirectionType.MvpnDirectionType == MvpnDirectionTypeEnum.Bidirectional 
                        && multicastVpnDirectionType.MvpnDirectionType == MvpnDirectionTypeEnum.Unidirectional)
                    {
                        foreach (var vpnAttachmentSet in currentVpn.VpnAttachmentSets)
                        {
                            if (vpnAttachmentSet.IsHub.GetValueOrDefault())
                            {
                                if (vpnAttachmentSet.AttachmentSet.MulticastVpnDomainType.Name != "Sender Only")
                                {
                                    ValidationDictionary.AddError(string.Empty, $"The Multicast Direction Type for VPN '{currentVpn.Name}' cannot be changed to "
                                        + $"Unidirectional because Attachment Set '{vpnAttachmentSet.AttachmentSet.Name}' is bound to the VPN and designated "
                                        + "as a Hub and therefore must be configured with a Multicast Domain Type of 'Sender Only'. Currently the Attachment Set "
                                        + $"is configured as '{vpnAttachmentSet.AttachmentSet.MulticastVpnDomainType.Name}'.");
                                }
                            }
                            else
                            {
                                if (vpnAttachmentSet.AttachmentSet.MulticastVpnDomainType.Name != "Receiver Only")
                                {
                                    ValidationDictionary.AddError(string.Empty, $"The Multicast Direction Type for VPN '{currentVpn.Name}' cannot be changed to "
                                        + $"Unidirectional because Attachment Set '{vpnAttachmentSet.AttachmentSet.Name}' is bound to the VPN and designated "
                                        + "as a Spoke and therefore must be configured with a Multicast Domain Type of 'Receiver Only'. Currently the Attachment Set "
                                        + $"is configured as '{vpnAttachmentSet.AttachmentSet.MulticastVpnDomainType.Name}'.");
                                }
                            }
                        }
                    }

                    if (currentVpn.IsExtranet && currentVpn.ExtranetVpnMembers.Any())
                    {
                        if (currentVpn.MulticastVpnDirectionType.MvpnDirectionType != multicastVpnDirectionType.MvpnDirectionType)
                        {
                            ValidationDictionary.AddError(string.Empty, "The Multicast VPN Direction Type cannot be changed because the VPN is a Hub-and-Spoke "
                                + "Extranet VPN and member VPNs are defined. The Multicast VPN Direction Type of Hub-and-Spoke Extranet VPNs can only be changed "
                                + "when there are no member VPNs.");
                        }
                    }
                    if (currentVpn.ExtranetVpns.Any())
                    {
                        if (currentVpn.MulticastVpnDirectionType.MvpnDirectionType != multicastVpnDirectionType.MvpnDirectionType)
                        {
                            foreach (var extranetVpn in currentVpn.ExtranetVpns)
                            {
                                ValidationDictionary.AddError(string.Empty, "The Multicast VPN Direction Type cannot be changed because the VPN is a Hub-and-Spoke "
                                    + $"Member VPN of Extranet VPN '{extranetVpn.ExtranetVpn.Name}'. The Multicast VPN Direction Type of VPN '{currentVpn.Name}' " 
                                    + "can be changed if the VPN does not belong to any Hub-and-Spoke Extranet VPNs.");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Validate deletion of a VPN.
        /// </summary>
        /// <param name="vpn"></param>
        public void ValidateDelete(Vpn vpn)
        {
            if (vpn.IsExtranet)
            {
                if (vpn.ExtranetVpnMembers.Any())
                {
                    ValidationDictionary.AddError(string.Empty, $"Extranet VPN '{vpn.Name}' cannot be deleted because member VPNs are defined. "
                        + "Remove the member VPNs from the Extranet first.");
                }
            }
        }

        /// <summary>
        /// Validates if a VPN can be synchronised with the network.
        /// </summary>
        /// <param name="vpn"></param>
        public void ValidateOkToSyncToNetwork(Vpn vpn)
        {
            var attachments = vpn.VpnAttachmentSets
                .SelectMany(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                .SelectMany(x => x.RoutingInstance.Attachments)
                .Where(x => x.RequiresSync)
                .GroupBy(x => x.AttachmentID).Select(x => x.First());

            var vifs = vpn.VpnAttachmentSets
                .SelectMany(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                .SelectMany(x => x.RoutingInstance.Vifs)
                .Where(x => x.RequiresSync)
                .GroupBy(x => x.VifID).Select(x => x.First());

            foreach (var attachment in attachments)
            {
                ValidationDictionary.AddError(string.Empty, $"Attachment '{attachment.Name}' which belongs to Tenant {attachment.Tenant.Name} requires "
                    + $"synchronisation with the network before VPN '{vpn.Name}' can be checked for synchronisation or synchronised with the network.");
            }

            foreach (var vif in vifs)
            {
                ValidationDictionary.AddError(string.Empty, $"VIF '{vif.Name}' which belongs to Tenant '{vif.Tenant.Name}' requires "
                    + $"synchronisation with the network before VPN '{vpn.Name}' can be checked for synchronisation or synchronised with the network.");
            }
        }
    }
}
