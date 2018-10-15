using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of IPv4 address and subnet mask
    /// </summary>
    public class Ipv4AddressAndMaskViewModel
    {
        /// <summary>
        /// IPv4 address
        /// </summary>
        /// <value>string value denoting an IPv4 address</value>
        /// <example>192.168.0.1</example>
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IPv4 address must be specified, e.g. 192.168.0.1")]
        public string IpAddress { get; set; }

        /// <summary>
        /// IPv4 subnet mask 
        /// </summary>
        /// <value>String value denoting an IPv4 subnet mask</value>
        /// <example>255.255.255.252</example>
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IPv4 subnet mask must be specified, e.g. 255.255.255.252")]
        public string SubnetMask { get; set; }
    }
}
