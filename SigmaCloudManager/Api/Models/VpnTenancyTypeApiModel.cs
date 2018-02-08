using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning VPN Tenancy Type data.
    /// </summary>
    public class VpnTenancyTypeApiModel
    {
        public int VpnTenancyTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string TenancyType { get; set; }
        public byte[] RowVersion { get; set; }
    }
}