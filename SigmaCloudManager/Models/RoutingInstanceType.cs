using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SCM.Models
{
    public class RoutingInstanceType
    {
        public int RoutingInstanceTypeID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsLayer3 { get; set; }
        public bool IsVrf { get; set; }
        public bool IsDefault { get; set; }
        public bool IsInfrastructureVrf { get; set; }
        public bool IsTenantFacingVrf { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<RoutingInstance> RoutingInstances { get; set; }
    }
}