using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models
{
    public class DeviceRolePortRole
    {
        public int DeviceRolePortRoleID { get; private set; }
        public int DeviceRoleID { get; set; }
        public int PortRoleID { get; set; }
        [Required]
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual DeviceRole DeviceRole { get; set; }
        public virtual PortRole PortRole { get; set; }
    }
}