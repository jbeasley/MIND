using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    public class MulticastVpnGroupRequestApiModel
    {
        [Required]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid multicast group address must be specified, e.g. 231.1.0.1")]
        public string IpAddress { get; set; }
        public int MulticastVpnRpID { get; set; }
    }
}