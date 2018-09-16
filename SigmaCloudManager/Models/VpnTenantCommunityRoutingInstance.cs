using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class VpnTenantCommunityRoutingInstance
    {
        public int VpnTenantCommunityRoutingInstanceID { get; private set; }
        public int? TenantCommunityID { get; set; }
        public int? TenantCommunitySetID { get; set; }
        public int AttachmentSetID { get; set; }
        public int RoutingInstanceID { get; set; }
        public int LocalIpRoutingPreference { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [ForeignKey("TenantCommunityID")]
        public virtual TenantCommunity TenantCommunity { get; set; }
        public virtual TenantCommunitySet TenantCommunitySet { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual RoutingInstance RoutingInstance { get; set; }
    }
}