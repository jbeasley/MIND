using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models
{
    public enum RouteTargetRangeEnum
    {
        Default = 1,
        Sigma = 2
    }

    public class RouteTargetRange
    {
        public int RouteTargetRangeID { get; private set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public RouteTargetRangeEnum Range { get; set; }
        public int AdministratorSubField { get; set; }
        public int AssignedNumberSubFieldStart { get; set; }
        public int AssignedNumberSubFieldCount { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<RouteTarget> RouteTargets  { get; set; }
    }
}