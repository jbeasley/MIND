using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.ViewModels
{
    public class MulticastVpnRpRequestApiModel
    {
        [Required]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IP address must be specified, e.g. 192.168.0.1")]
        public string IpAddress { get; set; }
        public int VpnID { get; set; }
    }
}