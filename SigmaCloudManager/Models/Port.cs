using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models
{
    public class Port
    {
        public int ID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{Type} {Name}";
            }
        }
        public int PortBandwidthID { get; set; }
        public int DeviceID { get; set; }
        public int? TenantID { get; set; }
        public int? InterfaceID { get; set; }
        public int PortSfpID { get; set; }
        public int PortConnectorID { get; set; }
        public int PortPoolID { get; set; }
        public int PortStatusID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual Device Device { get; set; }
        public virtual PortSfp PortSfp { get; set; }
        public virtual PortConnector PortConnector { get; set; }
        public virtual PortStatus PortStatus { get; set; }
        public virtual PortPool PortPool { get; set; }
        public virtual Interface Interface { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual PortBandwidth PortBandwidth { get; set; }
    }
}