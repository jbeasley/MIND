using Microsoft.EntityFrameworkCore;
using Mind.Models;
using SCM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;

namespace SCM.Models
{
    public static class AttachmentSetQueryExtensions
    {
        public static IQueryable<AttachmentSet> IncludeValidationProperties(this IQueryable<AttachmentSet> query)
        {
            return query.Include(x => x.MulticastVpnDomainType)
                        .Include(x => x.Region)
                        .Include(x => x.AttachmentRedundancy)
                        .Include(x => x.SubRegion)
                        .Include(x => x.Tenant)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Attachments)
                        .ThenInclude(x => x.Interfaces)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Vifs)
                        .ThenInclude(x => x.Vlans)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Device.Location.SubRegion.Region)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.RoutingInstance.Device.DeviceRole)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Device.Plane)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.Vpn)
                        .Include(x => x.VpnTenantMulticastGroups)
                        .Include(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.VpnTenantCommunitiesRoutingInstance)
                        .Include(x => x.VpnTenantIpNetworkRoutingInstanceStaticRoutes)
                        .ThenInclude(x => x.TenantIpNetwork);
        }

        public static IQueryable<AttachmentSet> IncludeDeepProperties(this IQueryable<AttachmentSet> query)
        {
            return query.Include(x => x.MulticastVpnDomainType)
                        .Include(x => x.Region)
                        .Include(x => x.AttachmentRedundancy)
                        .Include(x => x.SubRegion)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Attachments)
                        .ThenInclude(x => x.Interfaces)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Vifs)
                        .ThenInclude(x => x.Vlans)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.VpnTenantIpNetworkRoutingInstanceStaticRoutes)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksIn)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantCommunitiesIn)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Device.Location)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Device.Plane)
                        .Include(x => x.VpnAttachmentSets)
                        .Include(x => x.VpnTenantMulticastGroups)
                        .Include(x => x.VpnTenantCommunitiesIn)
                        .Include(x => x.VpnTenantCommunitiesOut)
                        .Include(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.VpnTenantCommunitiesRoutingInstance)
                        .Include(x => x.VpnTenantIpNetworkRoutingInstanceStaticRoutes)
                        .ThenInclude(x => x.TenantIpNetwork)
                        // Need a projection in order to filter static routes, IP networks, and communities in the attachment set
                        .Select(x => new AttachmentSet(x.AttachmentSetID)
                        {
                            AttachmentRedundancy = x.AttachmentRedundancy,
                            AttachmentRedundancyID = x.AttachmentRedundancyID,
                            AttachmentSetRoutingInstances = x.AttachmentSetRoutingInstances,
                            IsLayer3 = x.IsLayer3,
                            MulticastVpnDomainType = x.MulticastVpnDomainType,
                            MulticastVpnDomainTypeID = x.MulticastVpnDomainTypeID,
                            MulticastVpnRps = x.MulticastVpnRps,
                            Name = x.Name,
                            Region = x.Region,
                            RegionID = x.RegionID,
                            RowVersion = x.RowVersion,
                            SubRegion = x.SubRegion,
                            SubRegionID = x.SubRegionID,
                            Tenant = x.Tenant,
                            TenantID = x.TenantID,
                            VpnAttachmentSets = x.VpnAttachmentSets,
                            // The following gives us a result set with tenant communities which are to be added to 
                            // the inbound policy of all BGP peers in the attachment set
                            VpnTenantCommunitiesIn = x.VpnTenantCommunitiesIn
                                                      .Where(q => q.AddToAllBgpPeersInAttachmentSet).ToList(),
                            VpnTenantCommunitiesOut = x.VpnTenantCommunitiesOut,
                            VpnTenantCommunitiesRoutingInstance = x.VpnTenantCommunitiesRoutingInstance,
                            // The following gives us a result set with tenant IP networks which are to be added to 
                            // the inbound policy of all BGP peers in the attachment set
                            VpnTenantIpNetworksIn = x.VpnTenantIpNetworksIn
                                                     .Where(q => q.AddToAllBgpPeersInAttachmentSet).ToList(),
                            VpnTenantIpNetworksRoutingInstance = x.VpnTenantIpNetworksRoutingInstance,
                            VpnTenantMulticastGroups = x.VpnTenantMulticastGroups,
                            // The following gives us a result set with static routes which are to be added to all routing instances
                            // in the attachment set
                            VpnTenantIpNetworkRoutingInstanceStaticRoutes = x.VpnTenantIpNetworkRoutingInstanceStaticRoutes
                                                                             .Where(q => q.AddToAllRoutingInstancesInAttachmentSet).ToList()
                        });
        }
               
        /// <summary>
        /// Include all properties required to perform delete validation
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<AttachmentSet> IncludeDeleteValidationProperties(this IQueryable<AttachmentSet> query)
        {
            return query.Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.Vpn)
                        .Include(x => x.VpnTenantIpNetworksIn)
                        .Include(x => x.VpnTenantCommunitiesIn)
                        .Include(x => x.VpnTenantIpNetworksOut)
                        .Include(x => x.VpnTenantCommunitiesOut)
                        .Include(x => x.VpnTenantIpNetworksRoutingInstance)
                        .Include(x => x.VpnTenantMulticastGroups)
                        .Include(x => x.VpnTenantCommunitiesRoutingInstance)
                        .Include(x => x.VpnTenantIpNetworkRoutingInstanceStaticRoutes)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .Include(x => x.MulticastVpnRps)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.AttachmentSet.VpnAttachmentSets)
                        .ThenInclude(x => x.Vpn)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.VpnTenantIpNetworkRoutingInstances)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.VpnTenantCommunityRoutingInstances)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.VpnTenantIpNetworkRoutingInstanceStaticRoutes)
                        .ThenInclude(x => x.TenantIpNetwork);
        }
    }

    public class AttachmentSet : IModifiableResource
    {
        public AttachmentSet() { }
        public AttachmentSet(int attachmentSetId) => AttachmentSetID = attachmentSetId;

        public int AttachmentSetID { get; private set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public bool IsLayer3 { get; set; }
        public int TenantID { get; set; }
        public int AttachmentRedundancyID { get; set; }
        public int RegionID { get; set; }
        public int? SubRegionID { get; set; }
        public int? MulticastVpnDomainTypeID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual AttachmentRedundancy AttachmentRedundancy { get; set; }
        [ForeignKey("TenantID")]
        public virtual Tenant Tenant { get; set; }
        public virtual Region Region { get; set; }
        public virtual SubRegion SubRegion { get; set; }
        public virtual MulticastVpnDomainType MulticastVpnDomainType { get; set; }
        public virtual ICollection<AttachmentSetRoutingInstance> AttachmentSetRoutingInstances { get; set; }
        public virtual ICollection<VpnAttachmentSet> VpnAttachmentSets { get; set; }
        public virtual ICollection<VpnTenantIpNetworkIn> VpnTenantIpNetworksIn { get; set; }
        public virtual ICollection<VpnTenantIpNetworkRoutingInstanceStaticRoute> VpnTenantIpNetworkRoutingInstanceStaticRoutes { get; set; }
        public virtual ICollection<VpnTenantCommunityIn> VpnTenantCommunitiesIn { get; set; }
        public virtual ICollection<VpnTenantIpNetworkOut> VpnTenantIpNetworksOut { get; set; }
        public virtual ICollection<VpnTenantCommunityOut> VpnTenantCommunitiesOut { get; set; }
        public virtual ICollection<VpnTenantIpNetworkRoutingInstance> VpnTenantIpNetworksRoutingInstance { get; set; }
        public virtual ICollection<VpnTenantCommunityRoutingInstance> VpnTenantCommunitiesRoutingInstance { get; set; }
        public virtual ICollection<MulticastVpnRp> MulticastVpnRps { get; set; }
        public virtual ICollection<VpnTenantMulticastGroup> VpnTenantMulticastGroups { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the attachment set is setup correctly with the right number of routing instances, 
        /// depending on the attachment redundancy (i.e. bronze, silver, gold, custom).
        /// </summary>
        public virtual void ValidateAttachmentRedundancy()
        {
            if (!this.AttachmentSetRoutingInstances.Any())
                throw new IllegalStateException($"Attachment set '{this.Name}' requires routing instance associations. Currently no routing instances " +
                    $"are associated with the attachment set.");

            if (this.AttachmentRedundancy.AttachmentRedundancyType != AttachmentRedundancyTypeEnum.Custom)
            {
                (from attachmentSetRoutingInstances in this.AttachmentSetRoutingInstances
                 select attachmentSetRoutingInstances.RoutingInstance)
                         .ToList()
                         .ForEach(
                            routingInstance =>
                            {
                                var numLogicalAttachments = routingInstance.Attachments.Count() + routingInstance.Vifs.Count();
                                if (numLogicalAttachments > 1)
                                {
                                    throw new IllegalStateException($"Routing instance '{routingInstance.Name}' can be associated with at most one " +
                                        $"logical attachment but it is associated with multiple logical attachments. Use a 'custom' attachment Set " +
                                        $"if you wish to associate the routing instance with more than one logical attachment.");
                                }
                            }
                         );
            }

            if (this.AttachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Bronze)
            {
                if (this.AttachmentSetRoutingInstances.Count != 1)
                    throw new IllegalStateException($"Attachment set '{this.Name}' requires 1 routing instance association because " +
                        $"the attachment redundancy is configured for bronze-level redundancy.");

                var routingInstance = this.AttachmentSetRoutingInstances.First().RoutingInstance;
                if (this.SubRegion.SubRegionID != this.AttachmentSetRoutingInstances.First().RoutingInstance.Device.Location.SubRegionID)
                    throw new IllegalStateException($"Routing instance '{routingInstance.Name}' does not belong to the same sub-region as " +
                        $"attachment set '{this.Name}'");
            }
            else if (this.AttachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Silver)
            {
                if (this.AttachmentSetRoutingInstances.Count != 2)
                    throw new IllegalStateException($"Attachment set '{this.Name}' requires 2 routing instance associations because " +
                        $"the attachment redundancy is configured for silver-level redundancy.");

                var firstSilverRoutingInstance = this.AttachmentSetRoutingInstances.First().RoutingInstance;
                var secondSilverRoutingInstance = this.AttachmentSetRoutingInstances.ElementAt(1).RoutingInstance;
                if (firstSilverRoutingInstance.Device.LocationID != secondSilverRoutingInstance.Device.LocationID)
                {
                    throw new IllegalStateException($"The location property of each routing instance in attachment set '{this.Name}' " +
                        $"must be the same because the attachment set is configured for silver-level redundancy.");
                }

                if (firstSilverRoutingInstance.Device.Location.SubRegionID != this.SubRegion.SubRegionID)
                    throw new IllegalStateException("Routing instance " +
                        $"'{firstSilverRoutingInstance.Name}' does not belong to the same subregion " +
                        $"as attachment set '{this.Name}'.");

                if (secondSilverRoutingInstance.Device.Location.SubRegionID != this.SubRegion.SubRegionID)
                    throw new IllegalStateException("Routing instance " +
                        $"'{secondSilverRoutingInstance.Name}' does not belong to the same subregion " +
                        $"as attachment set '{this.Name}'.");

                if (firstSilverRoutingInstance.Device.Plane.PlaneID == secondSilverRoutingInstance.Device.Plane.PlaneID)
                {
                    throw new IllegalStateException($"Both routing instances in attachment set '{this.Name}' belong to the same provider plane " +
                        $"({firstSilverRoutingInstance.Device.Plane.Name}). A silver attachment set must be configured with routing instances which " +
                        $"belongs to different provider planes.");
                }
            }
            else if (this.AttachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Gold)
            {
                if (this.AttachmentSetRoutingInstances.Count != 2)
                    throw new IllegalStateException($"Attachment set '{this.Name}' requires 2 routing instance associations because " +
                        $"the attachment redundancy is configured for gold-level redundancy.");

                var firstGoldRoutingInstance = this.AttachmentSetRoutingInstances.First().RoutingInstance;
                var secondGoldRoutingInstance = this.AttachmentSetRoutingInstances.ElementAt(1).RoutingInstance;
                if (firstGoldRoutingInstance.Device.Location.SubRegionID != secondGoldRoutingInstance.Device.Location.SubRegionID)
                {
                    throw new IllegalStateException($"The subregion of each routing instance in attachment set '{this.Name}' " +
                        $"must be the same because the attachment set is configured for gold-level redundancy.");
                }

                if (firstGoldRoutingInstance.Device.Location.SubRegionID != this.SubRegion.SubRegionID)
                    throw new IllegalStateException($"Routing instance " +
                        $"'{firstGoldRoutingInstance.Name}' does not belong to the same subregion " +
                        $"as attachment set '{this.Name}'.");

                if (secondGoldRoutingInstance.Device.Location.SubRegionID != this.SubRegion.SubRegionID)
                    throw new IllegalStateException($"Routing instance " +
                        $"'{secondGoldRoutingInstance.Name}' does not belong to the same subregion " +
                        $"as attachment set '{this.Name}'.");

                if (firstGoldRoutingInstance.Device.LocationID == secondGoldRoutingInstance.Device.LocationID)
                {
                    throw new IllegalStateException($"The location of each routing instance in attachment set '{this.Name}' " +
                        $"must be different because the attachment set is configured for gold-level redundancy.");
                }

                if (firstGoldRoutingInstance.Device.Plane.PlaneID == secondGoldRoutingInstance.Device.Plane.PlaneID)
                {
                    throw new IllegalStateException($"Both routing instances in attachment set '{this.Name}' belong to the same provider plane " +
                        $"({firstGoldRoutingInstance.Device.Plane.Name}). A gold attachment set must be configured with routing instances which " +
                        $"belongs to different provider planes.");
                }
            }
        }

        /// <summary>
        /// Validate the state of the attachment set
        /// </summary>
        public virtual void Validate()
        {
            if (this.AttachmentRedundancy == null) throw new IllegalStateException($"An attachment redundancy option for attachment set " +
                $"'{this.Name}' must be defined.");

            if (this.VpnAttachmentSets.Any()) throw new IllegalStateException($"Attachment set '{this.Name}' is associated with " +
                $"one or more vpns. Remove the association with the vpns first, then make changes to the attachment set, then re-associate the attachment set " +
                $"with the vpns.");

            if (this.AttachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Bronze ||
            this.AttachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Silver ||
            this.AttachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Gold)
            {
                if (this.SubRegion == null) throw new IllegalStateException("A subregion must be defined for attachment set " +
                        $"'{this.Name}' with {this.AttachmentRedundancy.AttachmentRedundancyType.ToString()}-level redundancy");


            }
            else if (this.AttachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Custom)
            {
                if (this.AttachmentSetRoutingInstances.Select(
                    x =>
                        x.RoutingInstance.Device.Location.SubRegion.Region)
                    .Where(
                    x =>
                        x.RegionID != this.RegionID)
                    .Any())
                    throw new IllegalStateException($"All routing instances configured for custom attachment set '{this.Name}' must " +
                        $"belong to the same region as the attachment set.");

                if (this.SubRegion != null) throw new IllegalStateException("A subregion must not be defined for custom " +
                    $"attachment set '{this.Name}'.");
            }

            if (this.VpnAttachmentSets.Select(x => x.Vpn.IsMulticastVpn).Any())
            {
                if (this.MulticastVpnDomainType == null)
                {
                    throw new IllegalStateException($"A multicast domain type for attachment set '{this.Name}' must be defined because the " +
                        $"attachment set is associated with one or more multicast vpns.");
                }
            }

            if (this.MulticastVpnDomainType != null)
            {
                if (this.MulticastVpnDomainType.MvpnDomainType == MvpnDomainTypeEnum.ReceiverOnly)
                {
                    if (this.VpnTenantMulticastGroups.Any())
                    {
                        throw new IllegalStateException($"The multicast domain type for for attachment set '{this.Name}' cannot be 'Receiver-Only' " +
                            $"because multicast group ranges are associated with the attachment set. " +
                            "Remove the multicast group ranges from the attachment set first.");
                    }
                }
            }
        }

        /// <summary>
        /// Validate the attachment set can be deleted.
        /// </summary>
        public virtual void ValidateDelete()
        {
            var sb = new StringBuilder();
            this.VpnAttachmentSets
                .ToList()
                .ForEach(q =>
                        sb.Append($"Remove the attachment set '{this.Name}' from vpn '{q.Vpn.Name}' before trying to delete the attachment set.\n")
                 );

            if (sb.Length > 0) throw new IllegalDeleteAttemptException(sb.ToString());
        }
    }
}