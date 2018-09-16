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
    public static class VpnTenantIpNetworkInQueryableExtensions
    {
        public static IQueryable<VpnTenantIpNetworkIn> IncludeValidationProperties(this IQueryable<VpnTenantIpNetworkIn> query)
        {
            return query.Include(x => x.AttachmentSet)
                        .Include(x => x.TenantIpNetwork)
                        .Include(x => x.BgpPeer);
        }
    }

    public class VpnTenantIpNetworkIn : IModifiableResource
    {
        public int VpnTenantIpNetworkInID { get; private set; }
        public int TenantIpNetworkID { get; set; }
        public int AttachmentSetID { get; set; }
        public bool AddToAllBgpPeersInAttachmentSet { get; set; }
        public int? BgpPeerID { get; set; }
        public int? LocalIpRoutingPreference { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [ForeignKey("TenantIpNetworkID")]
        public virtual TenantIpNetwork TenantIpNetwork { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual BgpPeer BgpPeer { get; set; }
        public virtual ICollection<VpnTenantIpNetworkCommunityIn> VpnTenantIpNetworkCommunitiesIn { get; set; }
        public virtual ICollection<ExtranetVpnTenantNetworkIn> ExtranetVpnTenantNetworksIn { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the vpn tenant IP network.
        /// </summary>
        public void Validate()
        {
            if (this.AttachmentSet == null) throw new IllegalStateException("An attachment set association with the " +
                "tenant IP network is required but was not found.");

            if (this.TenantIpNetwork == null)
                throw new IllegalStateException("A tenant IP network is required but was not found.");

            if (this.AddToAllBgpPeersInAttachmentSet)
            {
                if (this.BgpPeer != null)
                {
                    throw new IllegalStateException($"A BGP peer association with the tenant IP network '{this.TenantIpNetwork.CidrName}' " +
                        "was found but is not required because the request is to add the tenant IP network to all bgp peers in attachment set " +
                        $"'{this.AttachmentSet.Name}'.");
                }
            }
            else
            {
                if (this.BgpPeer == null)
                {
                    throw new IllegalStateException($"A BGP peer association with the tenant IP network '{this.TenantIpNetwork.CidrName}' " +
                        "is required because the request is to add the tenant IP network to a specific bgp peer in attachment set " +
                        $"'{this.AttachmentSet.Name}'.");
                }
            }
        }
    }
}