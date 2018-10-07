using Microsoft.EntityFrameworkCore;
using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public static class VpnTenantCommunityOutQueryableExtensions
    {
        public static IQueryable<VpnTenantCommunityOut> IncludeValidationProperties(this IQueryable<VpnTenantCommunityOut> query)
        {
            return query.Include(x => x.AttachmentSet)
                        .Include(x => x.TenantCommunity)
                        .Include(x => x.BgpPeer.RoutingInstance.Device.DeviceRole);
        }

        public static IQueryable<VpnTenantCommunityOut> IncludeDeepProperties(this IQueryable<VpnTenantCommunityOut> query)
        {
            return query.Include(x => x.AttachmentSet.VpnAttachmentSets)
                        .Include(x => x.BgpPeer.RoutingInstance)
                        .Include(x => x.TenantCommunity);
        }
    }

    public class VpnTenantCommunityOut : IModifiableResource
    {
        public int VpnTenantCommunityOutID { get; private set; }
        public int TenantCommunityID { get; set; }
        public int? AttachmentSetID { get; set; }
        public int BgpPeerID { get; set; }
        public int AdvertisedIpRoutingPreference { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [ForeignKey("TenantCommunityID")]
        public virtual TenantCommunity TenantCommunity { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual BgpPeer BgpPeer { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the vpn tenant community.
        /// </summary>
        public virtual void Validate()
        {
            if (this.BgpPeer == null)
                throw new IllegalStateException($"A BGP peer association with the tenant community '{this.TenantCommunity.Name}' is required but " +
                    "was not found.");

            if (this.TenantCommunity == null)
                throw new IllegalStateException("A tenant community is required but was not found.");

            if (this.BgpPeer.RoutingInstance.Device.DeviceRole.IsProviderDomainRole)
            {
                if (this.AttachmentSet == null) throw new IllegalStateException("An attachment set association with the " +
                    "tenant community is required but was not found.");

                if (this.TenantCommunity.TenantID != this.AttachmentSet.TenantID)
                {
                    throw new IllegalStateException($"Tenant community '{this.TenantCommunity.Name}' cannot " +
                        $"be added to the outbound policy of attachment set '{this.AttachmentSet.Name}' because the tenant owner of the community and " +
                        $"the tenant owner of the attachment set are not the same.");
                }
            }

            if (this.BgpPeer.RoutingInstance.Device.DeviceRole.IsTenantDomainRole)
            {
                if (this.TenantCommunity.TenantID != this.BgpPeer.RoutingInstance.Device.TenantID)
                {
                    throw new IllegalStateException($"Tenant community '{this.TenantCommunity.Name}' cannot " +
                        $"be added to the outbound policy of BGP peer '{this.BgpPeer.Ipv4PeerAddress}' because the tenant owner of the community and " +
                        $"the tenant owner of the BGP peer are not the same.");
                }
            }
        }
    }
}