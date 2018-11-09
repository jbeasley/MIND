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
    /// Model of a tenant IP network which is a member of the BGP outbound policy of an attachment set
    /// </summary>
    public class VpnTenantIpNetworkOutViewModel
    {
        /// <summary>
        /// The ID of the vpn tenant IP network
        /// </summary>
        /// <value>Integer for the ID of the vpn tenant IP network</value>
        /// <example>8001</example>
        public int? VpnTenantIpNetworkOutId { get; private set; }

        /// <summary>
        /// The ID of the tenant IP network
        /// </summary>
        /// <value>The ID of the tenant IP network</value>
        /// <example>9001</example>
        public int? TenantIpNetworkId { get; private set; }

        /// <summary>
        /// The name of the attachment set to which the tenant IP network is associated.
        /// </summary>
        /// <value>A string value denoting the name of the attachment set</value>
        /// <example>713faafc85ff43db8472b6b9c38033a1</example>
        [Display(Name = "Attachment Set Name")]
        public string AttachmentSetName { get; private set; }

        /// <summary>
        /// The CIDR block representation of the tenant IP network
        /// </summary>
        /// <value>String representing the CIDR notation of the tenant IP network</value>
        /// <exampel>10.1.1.0/24 le 32</exampel>
        [Display(Name = "CIDR Name")]
        public string CidrName { get; private set; }

        /// <summary>
        /// An IPv4 BGP peer address from which the tenant IP network should be advertised
        /// </summary>
        /// <value>string representing the address of an existing configured IPv4 BGP peer</value>
        /// <example>192.168.0.1</example>
        [Display(Name = "IPv4 Peer Address")]
        public string Ipv4PeerAddress { get; private set; }

        /// <summary>
        /// The routing preference advertised with the route for the tenant IP network
        /// </summary>
        /// <value>An integer denoting the advertised IP routing preference</value>
        /// <example>10</example>
        [Display(Name="Advertised IP Routing Preference")]
        public int? AdvertisedIpRoutingPreference { get; private set; }
    }
}
