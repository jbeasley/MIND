using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning VPN Protocol Type data.
    /// </summary>
    public class VpnProtocolTypeApiModel
    {
        public int VpnProtocolTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string ProtocolType { get; set; }
        public byte[] RowVersion { get; set; }
    }
}