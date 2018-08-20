using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class VpnTenantNetworkRoutingInstance
    {
        public int VpnTenantNetworkRoutingInstanceID { get; set; }
        public int TenantNetworkID { get; set; }
        public int AttachmentSetID { get; set; }
        public int RoutingInstanceID { get; set; }
        public int LocalIpRoutingPreference { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual TenantIpNetwork TenantIpNetwork { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual RoutingInstance RoutingInstance { get; set; }
    }
}