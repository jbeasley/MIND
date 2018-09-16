using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class RoutingPolicyMatchOption
    {
        public int RoutingPolicyMatchOptionID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}