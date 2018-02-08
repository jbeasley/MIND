using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models
{
    public class PortPool
    {
        public int PortPoolID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public int PortRoleID { get; set; }
        [Required]
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual PortRole PortRole { get; set; }
        public virtual ICollection<Port> Ports { get; set; }
        public virtual ICollection<AttachmentRole> AttachmentRoles { get; set; }
    }
}