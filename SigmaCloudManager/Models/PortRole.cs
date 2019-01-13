using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mind.Models;

namespace SCM.Models
{
    public class PortRole
    {
        public int PortRoleID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public PortRoleTypeEnum PortRoleType { get; set; }
        [Required]
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<DeviceRolePortRole> DeviceRolePortRoles { get; set; }
        public virtual ICollection<PortPool> PortPools { get; set; }
    }
}