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
    /// Model for BGP peer requests and updates
    /// </summary>
    public class BgpPeerRequestViewModel
    {
        /// <summary>
        /// ID of the BGP peer
        /// </summary>
        /// <value>Integer value denoting the ID of the BGP peer</value>
        /// <example>1010</example>
        public int? BgpPeerId { get; set; }

        /// <summary>
        /// IPv4 address of the BGP peer
        /// </summary>
        /// <value>An IPv4 address</value>
        /// <example>12.1.1.1</example>
        [Display(Name = "IPv4 Peer Address")]
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IPv4 address must be specified, e.g. 192.168.0.1")]
        public string Ipv4PeerAddress { get; set; }

        /// <summary>
        /// The 2 Byte Autonomous system number of the BGP peer
        /// </summary>
        /// <value>An integer value between 1 and 65535</value>
        /// <example>65001</example>
        [Display(Name = "Peer 2-Byte Autonomous System")]
        [Required]
        [Range(1, 65535)]
        public int? Peer2ByteAutonomousSystem { get; set; }

        /// <summary>
        /// Authentication password for the BGP peer
        /// </summary>
        /// <value>A string representing the authentication password for the BGP peer</value>
        /// <example>pAs5w0rd!</example>
        [Display(Name = "Peer Password")]
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z0-9@\#\$%&\*\-\(\)\+\?\!\s\\/]+$")]
        [StringLength(30)]
        public string PeerPassword { get; set; }

        /// <summary>
        /// Determines if multi-hop peering is enabled. Defaults to false, meaning multihop is disabled.
        /// </summary>
        /// <value>Boolean value which determines if multi-hop peering is enabled</value>
        /// <example>true</example>
        [Display(Name = "MultiHop")]
        public bool IsMultiHop { get; set; }

        /// <summary>
        /// Determines if the peer should be enabled with bidirectional forwarding detection. Defaults to true, meaning BFD is enabled.
        /// </summary>
        /// <value>Boolean value which determines if the peer should be enabled with bidirectional forwarding detection</value>
        /// <example>true</example>
        [Display(Name = "BFD Enabled")]
        public bool IsBfdEnabled { get; set; } = true;

        /// <summary>
        /// Determines the maximum number of routes the peer should accept. The default is 500
        /// </summary>
        /// <value>A positive integer which determines the meximum number of routes accepted from the BGP peer</value>
        /// <example>200</example>
        [Display(Name = "Maximum Routes")]
        [Range(1, 1000)]
        public int? MaximumRoutes { get; set; } = 500;
    }
}
