using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class VpnTenantCommunityOut
    {
        public int VpnTenantCommunityOutID { get; set; }
        public int TenantCommunityID { get; set; }
        public int AttachmentSetID { get; set; }
        public int BgpPeerID { get; set; }
        public int AdvertisedIpRoutingPreference { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual TenantCommunity TenantCommunity { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual BgpPeer BgpPeer { get; set; }
    }
}