using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models
{
    public enum ProtocolTypeEnum
    {
        IP,
        Ethernet
    }

    public class VpnProtocolType
    {
        public int VpnProtocolTypeID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public ProtocolTypeEnum ProtocolType { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<VpnTopologyType> VpnTopologyTypes { get; set; }
    }
}