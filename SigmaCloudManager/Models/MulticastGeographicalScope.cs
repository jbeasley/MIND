using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models
{
    public class MulticastGeographicalScope
    {
        public int MulticastGeographicalScopeID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<VpnTenantMulticastGroup> VpnTenantMulticastGroups { get; set; }
    }
}