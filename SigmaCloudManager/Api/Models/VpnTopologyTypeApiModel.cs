using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning VPN Topology Type data.
    /// </summary>
    public class VpnTopologyTypeApiModel
    {
        public int VpnTopologyTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string TopologyType { get; set; }
        public int VpnProtocolTypeID { get; set; }
        public byte[] RowVersion { get; set; }
        public VpnProtocolTypeApiModel VpnProtocolType { get; set; }
    }
}