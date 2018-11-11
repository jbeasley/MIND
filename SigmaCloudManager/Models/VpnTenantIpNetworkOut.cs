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
            return query.Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksOut)
                        .Include(x => x.TenantIpNetwork)
                        .Include(x => x.BgpPeer.RoutingInstance.Device.DeviceRole)
                        .Include(x => x.AttachmentSet.VpnAttachmentSets)
                        .ThenInclude(x => x.Vpn)
                        .Include(x => x.AttachmentSet.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork);
        }

        public static IQueryable<VpnTenantIpNetworkOut> IncludeDeepProperties(this IQueryable<VpnTenantIpNetworkOut> query)
        {
            return query.Include(x => x.AttachmentSet.VpnAttachmentSets)
                        .Include(x => x.BgpPeer.RoutingInstance)
                        .Include(x => x.TenantIpNetwork.Tenant);
        }
    }

    public class VpnTenantIpNetworkOut : IModifiableResource
    {
        public int VpnTenantIpNetworkOutID { get; private set; }
        public int TenantIpNetworkID { get; set; }
        public int? AttachmentSetID { get; set; }
        public int? BgpPeerID { get; set; }
        public bool AddToAllBgpPeersInAttachmentSet { get; set; }
        public int AdvertisedIpRoutingPreference { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [ForeignKey("TenantIpNetworkID")]
        public virtual TenantIpNetwork TenantIpNetwork { get; set; }
        [ForeignKey("AttachmentSetID")]
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual BgpPeer BgpPeer { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();


        /// <summary>
        /// Validate the state of the vpn tenant IP network.
        /// </summary>
        public virtual void Validate()
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

                    if (this.BgpPeer.RoutingInstance.Device.DeviceRole.IsTenantDomainRole)
                    {
                        if (this.TenantIpNetwork.TenantID != this.BgpPeer.RoutingInstance.Device.TenantID)
                        {
                            throw new IllegalStateException($"Tenant IP network '{this.TenantIpNetwork.CidrNameIncludingIpv4LessThanOrEqualToLength}' cannot " +
                                $"be added to the outbound policy of BGP peer '{this.BgpPeer.Ipv4PeerAddress}' because the tenant owner of the IP network and " +
                                $"the tenant owner of the BGP peer are not the same.");
                        }
                    }
                }
            }
        }
    }
}