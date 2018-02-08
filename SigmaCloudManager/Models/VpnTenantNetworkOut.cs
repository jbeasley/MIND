using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class VpnTenantNetworkOut
    {
        public int VpnTenantNetworkOutID { get; set; }
        public int TenantNetworkID { get; set; }
        public int AttachmentSetID { get; set; }
        public int BgpPeerID { get; set; }
        public int AdvertisedIpRoutingPreference { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual TenantNetwork TenantNetwork { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual BgpPeer BgpPeer { get; set; }
    }
}