using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace SCM.Models
{
    public class Device
    {
        public int DeviceID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public int DeviceRoleID { get; set; }
        public int DeviceModelID { get; set; }
        public int DeviceStatusID { get; set; }
        public int? LocationID { get; set; }
        public int? PlaneID { get; set; }
        public int? TenantID { get; set; }
        public bool UseLayer2InterfaceMtu { get; set; }
        public bool Created { get; set; }
        public bool RequiresSync { get; set; }
        public bool ShowCreatedAlert { get; set; }
        public bool ShowRequiresSyncAlert { get; set; }
        [MaxLength(250)]
        public string Notes { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual DeviceRole DeviceRole { get; set; }
        public virtual DeviceModel DeviceModel { get; set; }
        public virtual DeviceStatus DeviceStatus { get; set; }
        public virtual Location Location { get; set;}
        public virtual Plane Plane { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<Port> Ports { get; set; }
        public virtual ICollection<Interface> Interfaces { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<RoutingInstance> RoutingInstances { get; set; }
    }
}