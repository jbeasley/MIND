
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
    /// Model for requesting a tenant IP network association with the outbound policy of an attachment set
    /// </summary>
    public class VpnTenantIpNetworkOutRequestViewModel
    {
        /// <summary>
        /// CIDR block name of the tenant IP network
        /// </summary>
        /// <value>String value for the CIDR representation of the tenant IP network</value>
        /// <example>10.1.1.0/24 le 32</example>
        [Display(Name = "IP Network CIDR Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage="A CIDR block range must be specified, e.g. 10.1.1.0/24. You can also include the " +
            "'less than or equal to' parameter, e.g. 10.1.1.0/24 le 32")]
        public string TenantIpNetworkCidrName { get; set; }

        /// <summary>
        /// An IPv4 BGP peer address from which the tenant IP network should advertised. THe specified BGP peer must be configured and exist
        /// within the attachment set.
        /// </summary>
        /// <value>string representing the address of an existing configured IPv4 BGP peer</value>
        /// <example>192.168.0.1</example>
        [Display(Name="IPv4 Peer Address")]
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", 
            ErrorMessage = "A valid IP address must be entered, e.g. 192.168.0.1")]
        public string Ipv4PeerAddress { get; set; }

        /// <summary>
        /// The routing preference to be advertised with the route for the tenant IP network. By default the value of this property is 1
        /// </summary>
        /// <value>Integer representing the advertised IP routing preference</value>
        /// <example>10</example>
        [Display(Name = "Advertised IP Routing Preference")]
        [Range(1, 20, ErrorMessage = "The advertised IP routing preference must be a number between 1 and 20")]
        public int? AdvertisedIpRoutingPreference { get; set; } = 1;

    }
}
