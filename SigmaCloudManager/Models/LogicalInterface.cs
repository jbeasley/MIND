using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace SCM.Models
{
    public enum LogicalInterfaceType {
    
        Loopback,
        Tunnel
    }

    public class LogicalInterface {

        [Key]
        public int LogicalInterfaceID { get; set; }
        [NotMapped]
        public string Name
        {
            get
            {
                return $"{LogicalInterfaceType} {ID}";
            }
        }
        public int RoutingInstanceID { get; set; }
        public int ID { get; set; }
        [MaxLength(15)]
        public string IpAddress { get; set; }
        [MaxLength(15)]
        public string SubnetMask { get; set; }
        public LogicalInterfaceType LogicalInterfaceType { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual RoutingInstance RoutingInstance { get; set; }
    }
}