using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models
{
    public enum TopologyTypeEnum
    {
        Meshed = 0,
        HubandSpoke = 1,
        PointtoPoint = 2,
        Multipoint = 3
    }

    public class VpnTopologyType
    {
        public int VpnTopologyTypeID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public TopologyTypeEnum TopologyType { get; set; }
        public int VpnProtocolTypeID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual VpnProtocolType VpnProtocolType { get; set; }
        public virtual ICollection<Vpn> Vpns { get; set; }
    }
}