using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models
{
    public enum RouteDistinguisherRangeTypeEnum
    {
        Default
        // Additional range types can be defined here 
    }

    public class RouteDistinguisherRange
    {
        public int RouteDistinguisherRangeID { get; private set; }
        [Required]
        public RouteDistinguisherRangeTypeEnum Type { get; set; }
        public int AdministratorSubField { get; set; }
        public int AssignedNumberSubFieldStart { get; set; }
        public int AssignedNumberSubFieldCount { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<RoutingInstance> RoutingInstances { get; set; }
    }
}