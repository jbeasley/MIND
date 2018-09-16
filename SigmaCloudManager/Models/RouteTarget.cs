using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models
{
    public class RouteTarget
    {
        public int RouteTargetID { get; private set; }
        [Required]
        public int AssignedNumberSubField { get; set; }
        public bool IsHubExport { get; set; }
        public int VpnID { get; set; }
        public int RouteTargetRangeID { get; set; }
        [NotMapped]
        public string Name
        {
            get
            {
                return $"{RouteTargetRange.AdministratorSubField}:{AssignedNumberSubField}";
            }
        }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual Vpn Vpn { get; set; }
        public virtual RouteTargetRange RouteTargetRange { get; set; }
    }
}