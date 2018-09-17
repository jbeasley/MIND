using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Mind.Models;

namespace SCM.Models
{
    public static class BgpPeerQueryableExtensions
    {
        public static IQueryable<BgpPeer> IncludeValidationProperties(this IQueryable<BgpPeer> query)
        {
            return query.Include(x => x.RoutingInstance)
                        .ThenInclude(x => x.Vifs)
                        .Include(x => x.RoutingInstance)
                        .ThenInclude(x => x.Attachments)
                        .Include(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.TenantIpNetwork);
        }

        public static IQueryable<BgpPeer> IncludeDeleteValidationProperties(this IQueryable<BgpPeer> query)
        {
            return query.Include(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.TenantIpNetwork);
        }

        public static IQueryable<BgpPeer> IncludeDeepProperties(this IQueryable<BgpPeer> query)
        {
            return query.Include(x => x.RoutingInstance)
                        .Include(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.TenantIpNetwork);
        }
    }

    public class BgpPeer : IModifiableResource
    {
        public int BgpPeerID { get; private set; }
        [MaxLength(15)]
        public string Ipv4PeerAddress { get; set; }
        public int Peer2ByteAutonomousSystem { get; set; }
        public int? MaximumRoutes { get; set; }
        public bool IsBfdEnabled { get; set; }
        public bool IsMultiHop { get; set; }
        [MaxLength(50)]
        public string PeerPassword { get; set; }
        [NotMapped]
        public string Name
        {
            get
            {
                return $"{Ipv4PeerAddress} (AS {Peer2ByteAutonomousSystem})";
            }
        }
        public int RoutingInstanceID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual RoutingInstance RoutingInstance { get; set; }
        public virtual ICollection<VpnTenantCommunityIn> VpnTenantCommunitiesIn { get; set; }
        public virtual ICollection<VpnTenantIpNetworkIn> VpnTenantIpNetworksIn { get; set; }
        public virtual ICollection<VpnTenantCommunityOut> VpnTenantCommunitiesOut { get; set; }
        public virtual ICollection<VpnTenantIpNetworkOut> VpnTenantIpNetworksOut { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the bgp peer
        /// </summary>
        public virtual void Validate()
        {
            if (this.RoutingInstance == null) throw new IllegalStateException("A routing instance for the BGP peer is required.");
            if (this.Peer2ByteAutonomousSystem < 1 || this.Peer2ByteAutonomousSystem > 65535)
                throw new IllegalStateException("The 2 byte autonomous system number requested is not valid. The number must be between " +
                    "1 and 65535.");

            if (!IPAddress.TryParse(this.Ipv4PeerAddress, out IPAddress peerIpv4Address))
                throw new IllegalStateException("The peer address is not a valid IPv4 address");

            // For non-multihop peers, the peer IP address must be reachable from at least one vif or attachment which
            // belongs to the routing instance
            if (!this.IsMultiHop)
            {
                var vif = this.RoutingInstance.Vifs.SelectMany(
                    x =>
                    x.Vlans)
                     .ToList()
                     .FirstOrDefault(
                        x =>
                        {
                            var network = IPNetwork.Parse(x.IpAddress, x.SubnetMask);
                            return network.Contains(peerIpv4Address);
                        });

                var attachment = this.RoutingInstance.Attachments.SelectMany(
                    x =>
                    x.Interfaces)
                     .ToList()
                     .FirstOrDefault(
                        x =>
                        {
                            var network = IPNetwork.Parse(x.IpAddress, x.SubnetMask);
                            return network.Contains(peerIpv4Address);
                        });

                if (vif == null && attachment == null)
                    throw new IllegalStateException($"The peer address '{this.Ipv4PeerAddress}' is not contained by any network which is " +
                        $"directly reachable from routing instance '{this.RoutingInstance.Name}'. Check that the IP address for at least one vif or " +
                        $"attachment belonging to the routing instance is in the same IPv4 network as the bgp peer.");
            }
        }

        /// <summary>
        /// Validate a bgp peer can be deleted
        /// </summary>
        public virtual void ValidateDelete()
        {
            var sb = new StringBuilder();
            this.VpnTenantCommunitiesIn
               .ToList()
               .ForEach(x =>
                   sb.Append("The BGP Peer cannot be deleted because community "
                   + $"'{x.TenantCommunity.Name}' is applied to the inbound policy of attachment set '{x.AttachmentSet.Name}'.\n")
            );

            this.VpnTenantCommunitiesOut
                .ToList()
                .ForEach(x =>
                    sb.Append("The BGP Peer cannot be deleted because community "
                    + $"'{x.TenantCommunity.Name}' is applied to the outbound policy of attachment set '{x.AttachmentSet.Name}'.\n")
                );

            this.VpnTenantIpNetworksIn
                .ToList()
                .ForEach(x =>
                    sb.Append("The BGP Peer cannot be deleted because IP network "
                    + $"'{x.TenantIpNetwork.CidrName}' is applied to the inbound policy of attachment set '{x.AttachmentSet.Name}'.\n")
                );

            this.VpnTenantIpNetworksOut
                .ToList()
                .ForEach(x =>
                    sb.Append("The BGP Peer cannot be deleted because IP network "
                    + $"'{x.TenantIpNetwork.CidrName}' is applied to the outbound policy of attachment set '{x.AttachmentSet.Name}'.\n")
                );

            if (sb.Length > 0) throw new IllegalDeleteAttemptException(sb.ToString());
        }
    }
}