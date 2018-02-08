using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for updating a VPN.
    /// </summary>
    public class VpnUpdateApiModel
    {
        public int VpnID { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public bool IsExtranet { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }
        public int? RegionID { get; set; }
        [Required]
        public int? VpnTenancyTypeID { get; set; }
        public int? MulticastVpnDirectionTypeID { get; set; }
    }
}