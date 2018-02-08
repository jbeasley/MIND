using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models
{
    public enum TenancyType
    {
        Single,
        Multi
    }

    public class VpnTenancyType
    {
        public int VpnTenancyTypeID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public TenancyType TenancyType { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<Vpn> Vpns { get; set; }
    }
}