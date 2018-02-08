using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models
{
    public class MulticastVpnRp
    {
        public int MulticastVpnRpID { get; set; }
        [Required]
        [MaxLength(15)]
        public string IpAddress { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public int AttachmentSetID { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual ICollection<VpnTenantMulticastGroup> VpnTenantMulticastGroups { get; set; }
    }
}