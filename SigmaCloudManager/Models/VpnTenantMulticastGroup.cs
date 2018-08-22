using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class VpnTenantMulticastGroup
    {
        public int VpnTenantMulticastGroupID { get; set; }
        public int TenantMulticastGroupID { get; set; }
        public int? MulticastVpnRpID { get; set; }
        public int AttachmentSetID { get; set; }
        public int? MulticastGeographicalScopeID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [ForeignKey("TenantMulticastGroupID")]
        public virtual TenantMulticastGroup TenantMulticastGroup { get; set; }
        public virtual MulticastVpnRp MulticastVpnRp { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual MulticastGeographicalScope MulticastGeographicalScope { get; set; }
    }
}