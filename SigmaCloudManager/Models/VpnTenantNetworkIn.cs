using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class VpnTenantNetworkIn
    {
        public int VpnTenantNetworkInID { get; set; }
        public int TenantNetworkID { get; set; }
        public int AttachmentSetID { get; set; }
        public bool AddToAllBgpPeersInAttachmentSet { get; set; }
        public int? BgpPeerID { get; set; }
        public int? LocalIpRoutingPreference { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual TenantNetwork TenantNetwork { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual BgpPeer BgpPeer { get; set; }
        public virtual ICollection<VpnTenantNetworkCommunityIn> VpnTenantNetworkCommunitiesIn { get; set; }
        public virtual ICollection<ExtranetVpnTenantNetworkIn> ExtranetVpnTenantNetworksIn { get; set; }
    }
}