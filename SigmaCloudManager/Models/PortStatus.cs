using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models
{
    public enum PortStatusType
    {
        Free,
        Assigned,
        Locked,
        Migration,
        Reserved
    }

    public class PortStatus
    {
        public int PortStatusID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public PortStatusType PortStatusType { get; set; }
        [Required]
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<Port> Ports { get; set; }
    }
}