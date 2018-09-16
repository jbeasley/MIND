using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models
{
    public class PortSfp
    {
        public int PortSfpID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [Required]
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<Port> Ports { get; set; }
    }
}