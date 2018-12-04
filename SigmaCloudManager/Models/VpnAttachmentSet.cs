using Microsoft.EntityFrameworkCore;
using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public static class VpnAttachmentSetQueryableExtensions
    {
        public static IQueryable<VpnAttachmentSet> IncludeValidationProperties(this IQueryable<VpnAttachmentSet> query)
        {
            return query.Include(x => x.Vpn.Plane)
                        .Include(x => x.Vpn.MulticastVpnDirectionType)
                        .Include(x => x.Vpn.MulticastVpnServiceType)
                        .Include(x => x.Vpn.VpnTopologyType)
                        .Include(x => x.AttachmentSet.AttachmentRedundancy)
                        .Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Device.Plane)
                        .Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Device.Location.SubRegion.Region)
                        .Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Attachments)
                        .Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Vifs)
                        .ThenInclude(x => x.Attachment)
                        .Include(x => x.AttachmentSet.VpnTenantMulticastGroups)
                        .ThenInclude(x => x.TenantMulticastGroup)
                        .Include(x => x.AttachmentSet.MulticastVpnDomainType);
        }

        public static IQueryable<VpnAttachmentSet> IncludeDeepProperties(this IQueryable<VpnAttachmentSet> query)
        {
            return query.Include(x => x.AttachmentSet.Tenant)
                        .Include(x => x.Vpn.VpnTopologyType)
                        .Include(x => x.Vpn.VpnTenancyType)
                        .Include(x => x.Vpn.MulticastVpnServiceType)
                        .Include(x => x.Vpn.MulticastVpnDirectionType)
                        .Include(x => x.Vpn.RouteTargets)
                        .ThenInclude(x => x.RouteTargetRange)
                        .Include(x => x.AttachmentSet.MulticastVpnDomainType)
                        .Include(x => x.AttachmentSet.Region)
                        .Include(x => x.AttachmentSet.AttachmentRedundancy)
                        .Include(x => x.AttachmentSet.SubRegion)
                        .Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Attachments)
                        .ThenInclude(x => x.Interfaces)
                        .Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Vifs)
                        .ThenInclude(x => x.Vlans)
                        .Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.VpnTenantIpNetworkRoutingInstanceStaticRoutes)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksIn)
                        .Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantCommunitiesIn)
                        .Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.AttachmentSet.VpnAttachmentSets)
                        .Include(x => x.AttachmentSet.VpnTenantMulticastGroups)
                        .Include(x => x.AttachmentSet.VpnTenantCommunitiesIn)
                        .Include(x => x.AttachmentSet.VpnTenantCommunitiesOut)
                        .Include(x => x.AttachmentSet.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.AttachmentSet.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.AttachmentSet.VpnTenantCommunitiesRoutingInstance)
                        .Include(x => x.AttachmentSet.VpnTenantIpNetworkRoutingInstanceStaticRoutes)
                        .ThenInclude(x => x.TenantIpNetwork)
                        // Need a projection in order to filter static routes, IP networks, and communities in the attachment set
                        .Select(x => new VpnAttachmentSet(x.VpnAttachmentSetID)
                        {
                            Vpn = x.Vpn,
                            VpnID = x.VpnID,
                            IsHub = x.IsHub,
                            RowVersion = x.RowVersion,
                            IsMulticastDirectlyIntegrated = x.IsMulticastDirectlyIntegrated,
                            AttachmentSetID = x.AttachmentSetID,
                            AttachmentSet = new AttachmentSet(x.AttachmentSetID)
                            {
                                AttachmentRedundancy = x.AttachmentSet.AttachmentRedundancy,
                                AttachmentRedundancyID = x.AttachmentSet.AttachmentRedundancyID,
                                AttachmentSetRoutingInstances = x.AttachmentSet.AttachmentSetRoutingInstances,
                                IsLayer3 = x.AttachmentSet.IsLayer3,
                                MulticastVpnDomainType = x.AttachmentSet.MulticastVpnDomainType,
                                MulticastVpnDomainTypeID = x.AttachmentSet.MulticastVpnDomainTypeID,
                                MulticastVpnRps = x.AttachmentSet.MulticastVpnRps,
                                Name = x.AttachmentSet.Name,
                                Region = x.AttachmentSet.Region,
                                RegionID = x.AttachmentSet.RegionID,
                                RowVersion = x.AttachmentSet.RowVersion,
                                SubRegion = x.AttachmentSet.SubRegion,
                                SubRegionID = x.AttachmentSet.SubRegionID,
                                Tenant = x.AttachmentSet.Tenant,
                                TenantID = x.AttachmentSet.TenantID,
                                // The following gives us a result set with tenant IP netwokrs which are to be added to 
                                // the inbound policy of all BGP peers in the attachment set
                                VpnTenantCommunitiesIn = x.AttachmentSet.VpnTenantCommunitiesIn
                                                                        .Where(q => q.AddToAllBgpPeersInAttachmentSet)
                                                                        .ToList(),
                                VpnTenantCommunitiesOut = x.AttachmentSet.VpnTenantCommunitiesOut,
                                VpnTenantCommunitiesRoutingInstance = x.AttachmentSet.VpnTenantCommunitiesRoutingInstance,
                                // The following gives us a result set with tenant communities which are to be added to 
                                // the inbound policy of all BGP peers in the attachment set
                                VpnTenantIpNetworksIn = x.AttachmentSet.VpnTenantIpNetworksIn
                                                                       .Where(q => q.AddToAllBgpPeersInAttachmentSet)
                                                                       .ToList(),
                                VpnTenantIpNetworksRoutingInstance = x.AttachmentSet.VpnTenantIpNetworksRoutingInstance,
                                VpnTenantMulticastGroups = x.AttachmentSet.VpnTenantMulticastGroups,
                                // The following gives us a result set with static routes which are to be added to all routing instances
                                // in the attachment set
                                VpnTenantIpNetworkRoutingInstanceStaticRoutes = x.AttachmentSet.VpnTenantIpNetworkRoutingInstanceStaticRoutes
                                                                                               .Where(q => q.AddToAllRoutingInstancesInAttachmentSet)
                                                                                               .ToList()
                            }
                        });

        }
    }

    public class VpnAttachmentSet : IModifiableResource
    {
        public VpnAttachmentSet() { }
        public VpnAttachmentSet(int vpnAttachmentSetId) => VpnAttachmentSetID = vpnAttachmentSetId;
        public int VpnAttachmentSetID { get; private set; }
        public int AttachmentSetID { get; set; }
        public int VpnID { get; set; }
        public bool? IsHub { get; set; }
        public bool? IsMulticastDirectlyIntegrated { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual Vpn Vpn { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the vpn attachment set
        /// </summary>
        protected virtual internal void Validate()
        {
            if (this.AttachmentSet == null) throw new IllegalStateException("An attachment set association is required.");
            if (this.Vpn == null) throw new IllegalStateException("A vpn association is required.");

            // If the attaachment set is to be directly integrated with the multicast domain of the tenant then the vpn must
            // be enabled for multicast
            if (this.IsMulticastDirectlyIntegrated.HasValue && this.IsMulticastDirectlyIntegrated.Value)
            {
                if (!this.Vpn.IsMulticastVpn) throw new IllegalStateException($"Attachment set '{this.AttachmentSet.Name}' " +
                    $"cannot be enabled for multicast-direct-integration with vpn '{this.Vpn.Name}' because the vpn is not enabled for " +
                    $"multicast.");
            }

            // Must not create an association of an attachment set with an extranet vpn
            if (this.Vpn.IsExtranet)
            {
                throw new IllegalStateException($"Attachment set '{this.AttachmentSet.Name}' cannot be added to extranet vpn " +
                    $"'{this.Vpn.Name}'. Instead, add vpns as members to the extranet vpn and then add communities and networks from the member " +
                    $"vpn to the extranet.");
            }

            // IsHub property must be set for Hub-and-Spoke VPN topologies only
            if (this.Vpn.VpnTopologyType.TopologyType == TopologyTypeEnum.HubandSpoke)
            {
                if (!this.IsHub.HasValue)
                {
                    throw new IllegalStateException($"The 'IsHub' property for attachment set '{this.AttachmentSet.Name}' must be specified because " +
                        $"vpn '{this.Vpn.Name}' is a hub-and-spoke IP VPN.");
                }
            }
            else
            {
                if (this.IsHub.GetValueOrDefault())
                {
                    throw new IllegalStateException($"The 'IsHub' property for attachment set '{this.AttachmentSet.Name}' must NOT be specified because " +
                        $"vpn '{this.Vpn.Name}' is not a hub-and-spoke IP VPN.");
                }
            }

            // Validate attachment set redundancy-level is correctly configured
            this.AttachmentSet.ValidateAttachmentRedundancy();

            if (this.Vpn.Plane != null)
            {
                // Checks to ensure the attachment set redundancy-level is compatible with the planar-scope of the vpn.
                // For example, a Silver-level redundancy attachment set has connectivity into both red and blue network planes and therefore
                // the vpn to which the attachment set is to be associated must not be scoped to one of those planes.
                var attachmentRedundancy = this.AttachmentSet.AttachmentRedundancy;
                if (attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Silver ||
                    attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Gold)
                {
                    throw new IllegalStateException($"Attachment set '{this.AttachmentSet.Name}' is configured for " +
                        $"'{attachmentRedundancy.Name}'-level redundancy which cannot be used with a planar-scoped vpn. The vpn is scoped to the " +
                        $"'{this.Vpn.Plane.Name}' plane. The attachment set provides connectivity into both planes.");
                }

                else if (attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Bronze)
                {
                    var attachmentPlane = this.AttachmentSet.AttachmentSetRoutingInstances.Single().RoutingInstance.Device.Plane.Name;
                    if (attachmentPlane != this.Vpn.Plane.Name)
                    {
                        throw new IllegalStateException($"Attachment set '{this.AttachmentSet.Name}' is configured for " +
                            $"'{attachmentRedundancy.Name}'-level redundancy which cannot be used with vpn '{this.Vpn.Name}' because the vpn " +
                            $"is scoped to the '{this.Vpn.Plane.Name}' plane. The attachment set provides connectivity into the " +
                            $"'{attachmentPlane}' plane only.");
                    }
                }

                else if (attachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Custom)
                {
                    if (this.AttachmentSet.AttachmentSetRoutingInstances
                            .Count(
                                x =>
                                x.RoutingInstance.Device.Plane.Name == this.Vpn.Plane.Name) != this.AttachmentSet.AttachmentSetRoutingInstances
                            .Count())
                    {
                        throw new IllegalStateException($"Vpn '{this.Vpn.Name}' is scoped to the '{this.Vpn.Plane.Name}' plane. " +
                            $"One or more routing instances which belong to attachment set '{this.AttachmentSet.Name}' do not belong to the same plane. " +
                            $"These routing instances must be removed from the attachment set before you can associate the attachment set with the vpn.");
                    }
                }

                // Check to validate the attachment set for a multicast vpn
                if (this.Vpn.IsMulticastVpn)
                {
                    var multiportAttachment = this.AttachmentSet.AttachmentSetRoutingInstances
                        .SelectMany(
                            x =>
                            x.RoutingInstance.Attachments)
                        .FirstOrDefault(
                            x =>
                            x.IsMultiPort);

                    if (multiportAttachment != null) throw new IllegalStateException($"Attachment set '{this.AttachmentSet.Name}' cannot be " +
                        $"associated with multicast vpn '{this.Vpn.Name}' because routing instance '{multiportAttachment.RoutingInstance.Name}' belongs " +
                        $"to the attachment set and the routing instance has a multiport attachment associated with it. Multiport attachments do not currently support " +
                        $"multicast services.");

                    var multiportAttachmentVif = this.AttachmentSet.AttachmentSetRoutingInstances
                        .SelectMany(
                            x =>
                            x.RoutingInstance.Vifs)
                        .FirstOrDefault(
                            x =>
                            x.Attachment.IsMultiPort);

                    if (multiportAttachmentVif != null) throw new IllegalStateException($"Attachment set '{this.AttachmentSet.Name}' cannot be " +
                       $"associated with multicast vpn '{this.Vpn.Name}' because routing instance '{multiportAttachment.RoutingInstance.Name}' belongs " +
                       $"to the attachment set and the routing instance has a vif which belongs to a multiport attachment associated with it. Multiport attachments do not currently support " +
                       $"multicast services.");

                    if (this.AttachmentSet.MulticastVpnDomainTypeID == null) throw new IllegalStateException($"Attachment Set " +
                        $"'{this.AttachmentSet.Name}' requires a multicast domain type option to be set before it can be associated with " +
                        $"multicast vpn '{this.Vpn.Name}'.");

                    var multicastVpnDomainType = this.AttachmentSet.MulticastVpnDomainType;

                    if (this.Vpn.VpnTopologyType.TopologyType == SCM.Models.TopologyTypeEnum.HubandSpoke)
                    {
                        var multicastVpnDirectionType = this.Vpn.MulticastVpnDirectionType.MvpnDirectionType;
                        if (multicastVpnDirectionType == MvpnDirectionTypeEnum.Unidirectional)
                        {
                            // Checks for a undirectional hub-and-spoke multicast vpn
                            if (this.IsHub.GetValueOrDefault())
                            {
                                if (this.AttachmentSet.MulticastVpnDomainType.MvpnDomainType != MvpnDomainTypeEnum.SenderOnly)
                                {
                                    throw new IllegalStateException($"Vpn '{this.Vpn.Name}' is a unidirectional hub-and-spoke multicast vpn and "
                                        + $"therefore the multicast vpn domain type for attachment set '{this.AttachmentSet.Name}' must be " +
                                        $"'Sender-Only' because the attachment set is designated as a HUB.");
                                }
                            }
                            else
                            {
                                if (multicastVpnDomainType.MvpnDomainType != MvpnDomainTypeEnum.ReceiverOnly)
                                {
                                    throw new IllegalStateException($"Vpn '{this.Vpn.Name}' is a unidirectional hub-and-spoke multicast vpn and "
                                        + $"therefore the multicast vpn domain type for attachment set '{this.AttachmentSet.Name}' must be " +
                                        $"'Receiver-Only' because the attachment set is designated as a SPOKE.");
                                }
                            }
                        }
                        else
                        {
                            // Checks for a bidirectional hub-and-spoke multicast vpn
                            if (this.IsHub.GetValueOrDefault())
                            {
                                if (multicastVpnDomainType.MvpnDomainType != MvpnDomainTypeEnum.SenderOnly &&
                                    multicastVpnDomainType.MvpnDomainType != MvpnDomainTypeEnum.SenderAndReceiver)
                                {
                                    throw new IllegalStateException($"Vpn '{this.Vpn.Name}' is a bidirectional hub-and-spoke multicast vpn and "
                                        + $"therefore the multicast vpn domain type for attachment set '{this.AttachmentSet.Name}' must be either "
                                        + "'Sender-Only' or 'Sender-and-Receiver' because the attachment set is designated as a HUB.");
                                }
                            }
                            else
                            {
                                if (multicastVpnDomainType.MvpnDomainType != MvpnDomainTypeEnum.ReceiverOnly &&
                                    multicastVpnDomainType.MvpnDomainType != MvpnDomainTypeEnum.SenderAndReceiver)
                                {
                                    throw new IllegalStateException($"Vpn '{this.Vpn.Name}' is a bidirectional hub-and-spoke multicast vpn and "
                                        + $"therefore the multicast vpn domain type for attachment set '{this.AttachmentSet.Name}' must be "
                                        + "'Receiver-Only' or 'Sender-and-Receiver' because the attachment set is designated as a SPOKE.");
                                }
                            }
                        }
                    }

                    if (this.Vpn.MulticastVpnServiceType.MvpnServiceType == MvpnServiceTypeEnum.ASM)
                    {
                        if (this.AttachmentSet.VpnTenantMulticastGroups
                            .Any(
                            x =>
                            x.TenantMulticastGroup.IsSsmGroup))
                        {
                            throw new IllegalStateException($"Attachment set '{this.AttachmentSet.Name}' cannot be added to vpn " +
                                $"'{this.Vpn.Name}' because there are source-specific multicast group ranges associated with the attachment set and " +
                                $"the multicast service type of the vpn is ASM. Remove the SSM group ranges from the attachment set first, or create a new attachment set.");
                        }
                    }
                    else if (this.Vpn.MulticastVpnServiceType.MvpnServiceType == MvpnServiceTypeEnum.SSM)
                    {
                        if (this.AttachmentSet.VpnTenantMulticastGroups
                            .Any(
                            x =>
                            !x.TenantMulticastGroup.IsSsmGroup))
                        {
                            throw new IllegalStateException($"Attachment set '{this.AttachmentSet.Name}' cannot be added to vpn " +
                                $"'{this.Vpn.Name}' because there are any-source multicast group ranges associated with the attachment set and the " +
                                $"multicast service type of the VPN is SSM. Remove the ASM group ranges from the attachment set, or create a new attachment set.");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Validate deletion of the vpn attachment set
        /// </summary>
        protected virtual internal void ValidateDelete()
        {
            if (this.Vpn.NetworkStatus == NetworkStatusEnum.Staged)
            {

            }
        }
    }
}