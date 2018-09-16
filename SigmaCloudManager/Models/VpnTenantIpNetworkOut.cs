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
    public static class VpnTenantIpNetworkOutQueryableExtensions
    {
        public static IQueryable<VpnTenantIpNetworkOut> IncludeValidationProperties(this IQueryable<VpnTenantIpNetworkOut> query)
        {
            return query.Include(x => x.AttachmentSet)
                        .Include(x => x.TenantIpNetwork)
                        .Include(x => x.BgpPeer);
        }
    }

    public class VpnTenantIpNetworkOut : IModifiableResource
    {
        public int VpnTenantIpNetworkOutID { get; private set; }
        public int TenantIpNetworkID { get; set; }
        public int AttachmentSetID { get; set; }
        public int BgpPeerID { get; set; }
        public int AdvertisedIpRoutingPreference { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [ForeignKey("TenantIpNetworkID")]
        public virtual TenantIpNetwork TenantIpNetwork { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual BgpPeer BgpPeer { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();


        /// <summary>
        /// Validate the state of the vpn tenant IP network.
        /// </summary>
        public virtual void Validate()
        {
            if (this.AttachmentSet == null) throw new IllegalStateException("An attachment set association with the " +
                "tenant IP network is required but was not found.");

            if (this.TenantIpNetwork == null)
                throw new IllegalStateException("A tenant IP network is required but was not found.");

            if (this.BgpPeer == null)
                throw new IllegalStateException($"A BGP peer association with the tenant IP network '{this.TenantIpNetwork.CidrName}' " +
                    "was not found.");
        }
    }
}