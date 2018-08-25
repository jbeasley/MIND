using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using Mind.Models;

namespace SCM.Models
{
    public class BgpPeer : IModifiableResource
    {
        public int BgpPeerID { get; set; }
        [MaxLength(15)]
        public string Ipv4PeerAddress { get; set; }
        [Required]
        [Range(1,65535)]
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
    }
}