using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for a Tenant Community request.
    /// </summary>
    public class TenantCommunityRequestApiModel
    {
        [Range(1,65535, ErrorMessage = "An Autonomous System number between 1 and 65535 must be specified.")]
        public int AutonomousSystemNumber { get; set; }
        [Required]
        [Range(1,4294967295, ErrorMessage ="A number between 1 and 4294967295 must be specified.")]
        public int Number { get; set; }
        public bool AllowExtranet { get; set; }
        [Required]
        public int? TenantID { get; set; }
    }
}