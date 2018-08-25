using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class VpnTenantIpNetworkRoutingInstance : IModifiableResource
    {
        public int VpnTenantIpNetworkRoutingInstanceID { get; set; }
        public int TenantIpNetworkID { get; set; }
        public int AttachmentSetID { get; set; }
        public int RoutingInstanceID { get; set; }
        public int LocalIpRoutingPreference { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [ForeignKey("TenantIpNetworkID")]
        public virtual TenantIpNetwork TenantIpNetwork { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual RoutingInstance RoutingInstance { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();
    }
}