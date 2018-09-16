using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models
{
    public class TenantMulticastGroup
    {
        public int TenantMulticastGroupID { get; private set; }
        [MaxLength(15)]
        public string SourceAddress { get; set; }
        [MaxLength(15)]
        public string SourceMask { get; set; }
        [Required]
        [MaxLength(15)]
        public string GroupAddress { get; set; }
        [MaxLength(15)]
        public string GroupMask { get; set; }
        public bool IsSsmGroup { get; set; }
        public bool AllowExtranet { get; set; }
        [Required]
        public int TenantID { get; set; }
        [NotMapped]
        public string Name { get
            {
                if (IsSsmGroup)
                {
                    return $"{SourceAddress}/{SourceMask} {GroupAddress}/{GroupMask}";
                }
                else
                {
                    return $"{GroupAddress}/{ GroupMask}";
                }
            }
        }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<VpnTenantMulticastGroup> VpnTenantMulticastGroups { get; set; }
    }
}