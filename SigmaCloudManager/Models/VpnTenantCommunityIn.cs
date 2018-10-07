using Microsoft.EntityFrameworkCore;
using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;

namespace SCM.Models
{
    public static class VpnTenanCommunityInQueryableExtensions
    {
        public static IQueryable<VpnTenantCommunityIn> IncludeValidationProperties(this IQueryable<VpnTenantCommunityIn> query)
        {
            return query.Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .Include(x => x.TenantCommunity)
                        .Include(x => x.BgpPeer.RoutingInstance.Device.DeviceRole)
                        .Include(x => x.AttachmentSet.VpnAttachmentSets)
                        .ThenInclude(x => x.Vpn)
                        .Include(x => x.AttachmentSet.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.TenantCommunity);
        }

        public static IQueryable<VpnTenantCommunityIn> IncludeDeepProperties(this IQueryable<VpnTenantCommunityIn> query)
        {
            return query.Include(x => x.AttachmentSet.VpnAttachmentSets)
                        .Include(x => x.BgpPeer.RoutingInstance)
                        .Include(x => x.TenantCommunity)
                        .Include(x => x.ExtranetVpnTenantCommunitiesIn);
        }
    }

    public class VpnTenantCommunityIn: IModifiableResource
    {
        public int VpnTenantCommunityInID { get; private set; }
        public int TenantCommunityID { get; set; }
        public int? AttachmentSetID { get; set; }
        public bool AddToAllBgpPeersInAttachmentSet { get; set; }
        public int? BgpPeerID { get; set; }
        public int? LocalIpRoutingPreference { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [ForeignKey("TenantCommunityID")]
        public virtual TenantCommunity TenantCommunity { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual BgpPeer BgpPeer { get; set; }
        public virtual ICollection<ExtranetVpnTenantCommunityIn> ExtranetVpnTenantCommunitiesIn { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the vpn tenant community.
        /// </summary>
        public void Validate()
        {
            if (this.TenantCommunity == null)
                throw new IllegalStateException("A tenant community is required but was not found.");

            if (this.BgpPeer != null)
            {
                if (this.BgpPeer.RoutingInstance.Device.DeviceRole.IsProviderDomainRole)
                {
                    if (this.AttachmentSet == null)
                    {
                        throw new IllegalStateException("An attachment set association with the " +
                            "tenant community is required but was not found.");
                    }
                }
                else if (this.BgpPeer.RoutingInstance.Device.DeviceRole.IsTenantDomainRole)
                {
                    if (this.AttachmentSet != null)
                    {
                        throw new IllegalStateException("An attachment set association was found but is not required.");
                    }
                }
            }

            if (this.AttachmentSet == null && this.BgpPeer == null)
            {
                throw new IllegalStateException("The tenant community must be associated with an existing attachment set, " +
                    "an existing BGP peer, or both.");
            }

            if (this.AddToAllBgpPeersInAttachmentSet)
            {
                if (this.BgpPeer != null)
                {
                    throw new IllegalStateException($"A BGP peer association with the tenant community '{this.TenantCommunity.Name}' " +
                        "was found but is not required because the request is to add the tenant community to all bgp peers in attachment set " +
                        $"'{this.AttachmentSet.Name}'.");
                }

                // Cannot associate the tenant community with all BGP peers in the attachment set and also a specific BGP peer concurrently
                var bgpPeer = (from attachmentSetRoutingInstances in this.AttachmentSet.AttachmentSetRoutingInstances
                               from peers in attachmentSetRoutingInstances.RoutingInstance.BgpPeers
                               from tenantIpNetworks in peers.VpnTenantIpNetworksIn
                               where TenantCommunityID == this.TenantCommunity.TenantCommunityID
                               select peers)
                                       .SingleOrDefault();
                if (bgpPeer != null)
                {
                    throw new IllegalStateException($"Tenant community '{this.TenantCommunity.Name}' is " +
                        $"already associated with BGP peer '{bgpPeer.Ipv4PeerAddress}' of attachment set '{this.AttachmentSet.Name}'.");
                }
            }
            else
            {
                if (this.BgpPeer == null)
                {
                    throw new IllegalStateException($"A BGP peer association with the tenant community '{this.TenantCommunity.Name}' " +
                        $"is required but was not found. A BGP peer which belongs to attachment set '{this.AttachmentSet.Name}' is required.");
                }

                if (this.BgpPeer.RoutingInstance.Device.DeviceRole.IsProviderDomainRole)
                {
                    // Cannot associate the tenant community with a specific BGP peer and also all BGP peers in the attachment set concurrently
                    if (this.AttachmentSet.VpnTenantCommunitiesIn
                                          .Where(
                                              x => 
                                                // Ignore the current item - we're only looking for other items for the same community ID
                                                 x.VpnTenantCommunityInID != this.VpnTenantCommunityInID && 
                                                 x.TenantCommunity.TenantCommunityID == this.TenantCommunity.TenantCommunityID)
                                          .Any())
                    {
                        throw new IllegalStateException($"Tenant community '{this.TenantCommunity.Name}' is " +
                            $"already associated with all BGP peers of attachment set '{this.AttachmentSet.Name}'.");
                    }
                }
            }

            if (this.AttachmentSet != null)
            {
                var sb = new StringBuilder();
                (from result in this.AttachmentSet.VpnAttachmentSets.Where(
                 q =>
                 q.AttachmentSetID == this.AttachmentSetID && q.Vpn.IsExtranet)
                 select result.Vpn)
                 .ToList()
                 .ForEach(
                    x =>
                        sb.Append($"Tenant community '{this.TenantCommunity.Name}' " +
                                   $"cannot be added to the inbound policy of attachment set '{this.AttachmentSet.Name}' because the attachment set " +
                                   $"is associated with extranet vpn '{x.Name}' and the tenant community is not enabled for extranet. Update the tenant " +
                                   $"community to enable it for extranet services first.").Append("\r\n")
                        );

                if (sb.Length > 0) throw new IllegalStateException(sb.ToString());
            }
        }
    }
}