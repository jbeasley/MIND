using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class DeviceRole
    {
        public int DeviceRoleID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public bool IsTenantDomainRole { get; set; }
        public bool IsProviderDomainRole { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<DeviceRoleAttachmentRole> DeviceRoleAttachmentRoles { get; set; }
        public virtual ICollection<DeviceRolePortRole> DeviceRolePortRoles { get; set; }
    }
}