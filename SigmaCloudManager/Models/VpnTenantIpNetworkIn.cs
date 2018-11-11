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
    public static class VpnTenantIpNetworkInQueryableExtensions
    {
        public static IQueryable<VpnTenantIpNetworkIn> IncludeValidationProperties(this IQueryable<VpnTenantIpNetworkIn> query)
        {
            return query.Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.BgpPeer.RoutingInstance.Device.DeviceRole)
                        .Include(x => x.AttachmentSet.VpnAttachmentSets)
                        .ThenInclude(x => x.Vpn)
                        .Include(x => x.AttachmentSet.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork);
        }

        public static IQueryable<VpnTenantIpNetworkIn> IncludeDeepProperties(this IQueryable<VpnTenantIpNetworkIn> query)
        {
            return query.Include(x => x.AttachmentSet.VpnAttachmentSets)
                        .Include(x => x.BgpPeer.RoutingInstance)
                        .Include(x => x.TenantIpNetwork)
                        .Include(x => x.VpnTenantIpNetworkCommunitiesIn)
                        .Include(x => x.ExtranetVpnTenantNetworksIn);            
        }
    }

    public class VpnTenantIpNetworkIn : IModifiableResource
    {
        public int VpnTenantIpNetworkInID { get; private set; }
        public int TenantIpNetworkID { get; set; }
        public int? AttachmentSetID { get; set; }
        public bool AddToAllBgpPeersInAttachmentSet { get; set; }
        public int? BgpPeerID { get; set; }
        public int? LocalIpRoutingPreference { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [ForeignKey("TenantIpNetworkID")]
        public virtual TenantIpNetwork TenantIpNetwork { get; set; }
        [ForeignKey("AttachmentSetID")]
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
            if (this.TenantIpNetwork == null)
                throw new IllegalStateException("A tenant IP network is required but was not found.");

            if (this.BgpPeer != null)
            {
                if (this.BgpPeer.RoutingInstance.Device.DeviceRole.IsProviderDomainRole)
                {
                    if (this.AttachmentSet == null)
                    {
                        throw new IllegalStateException("An attachment set association with the " +
                            "tenant IP network is required but was not found.");
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
                throw new IllegalStateException("The tenant IP network must be associated with an existing attachment set, " +
                    "an existing BGP peer, or both.");
            }

            if (this.AddToAllBgpPeersInAttachmentSet)
            {
                if (this.BgpPeer != null)
                {
                    throw new IllegalStateException($"A BGP peer association with the tenant IP network '{this.TenantIpNetwork.CidrNameIncludingIpv4LessThanOrEqualToLength}' " +
                        "was found but is not required because the request is to add the tenant IP network to all bgp peers in attachment set " +
                        $"'{this.AttachmentSet.Name}'.");
                }
            }
            else
            {
                if (this.BgpPeer == null)
                {
                    throw new IllegalStateException($"A BGP peer association with the tenant IP network '{this.TenantIpNetwork.CidrNameIncludingIpv4LessThanOrEqualToLength}' " +
                        $"is required but was not found. A BGP peer which belongs to attachment set '{this.AttachmentSet.Name}' is required.");
                }
                else
                {
                    // The BGP peer must belong to a routing instance which is associated with the attachment set
                    if (!this.AttachmentSet.AttachmentSetRoutingInstances
                        .Select(
                            attachmentSetRoutingInstance =>
                            attachmentSetRoutingInstance.RoutingInstance.Name)
                        .ToList()
                        .Contains(
                            this.BgpPeer.RoutingInstance.Name))
                    {
                        throw new IllegalStateException($"BGP peer '{this.BgpPeer.Name}' belongs to routing instance '{this.BgpPeer.RoutingInstance.Name}' but this " +
                       $"routing instance is not associated with attachment set '{this.AttachmentSet.Name}'. Make sure the routing instance is associated with that attachment set " +
                       $"or select another BGP peer.");
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
                        sb.Append($"Tenant IP network '{this.TenantIpNetwork.CidrName}' " +
                                   $"cannot be added to the inbound policy of attachment set '{this.AttachmentSet.Name}' because the attachment set " +
                                   $"is associated with extranet vpn '{x.Name}' and the tenant IP network is not enabled for extranet. Update the tenant " +
                                   $"IP network to enable it for extranet services first.").Append("\r\n")
                        );

                if (sb.Length > 0) throw new IllegalStateException(sb.ToString());
            }
        }
    }
}