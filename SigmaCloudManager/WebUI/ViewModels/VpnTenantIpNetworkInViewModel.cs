
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
    /// Model of a tenant IP network which is a member of the BGP inbound policy of an attachment set
    /// </summary>
    public class VpnTenantIpNetworkInViewModel
    {
        /// <summary>
        /// The ID of the tenant owner of the IP network which is associated with the inbound
        /// policy of the attachment set.
        /// </summary>
        /// <value>An integer denoting the ID of the tenant</value>
        /// <example>1001</example>
        public int? TenantId { get; private set; }

        /// <summary>
        /// The ID of the vpn tenant IP network
        /// </summary>
        /// <value>Integer for the ID of the vpn tenant IP network</value>
        /// <example>8001</example>
        public int? VpnTenantIpNetworkInId { get; private set; }

        /// <summary>
        /// The ID of the tenant IP network
        /// </summary>
        /// <value>Integer for the ID of the tenant IP network</value>
        /// <example>9001</example>
        public int? TenantIpNetworkId { get; private set; }

        /// <summary>
        /// The CIDR block representation of the tenant IP network
        /// </summary>
        /// <value>String representing the CIDR notation of the tenant IP network</value>
        /// <exampel>10.1.1.0/24 le 32</exampel>
        [Display(Name = "CIDR Name")]
        public string TenantIpNetworkCidrName { get; private set; }

        /// <summary>
        /// Denotes whether the tenant IP network is learned from all BGP peers that are configured within the attachment set
        /// </summary>
        /// <value>Boolean denoting whether the tenant IP network is learned from all BGP peers that exist within the attachment set</value>
        /// <example>true</example>
        [Display(Name = "Associated with all BGP Peers in Attachment Set")]
        public bool AssociatedWithAllBgpPeersInAttachmentSet { get; private set; }

        /// <summary>
        /// An IPv4 BGP peer address from which the tenant IP network should be learned
        /// </summary>
        /// <value>string representing the address of an existing configured IPv4 BGP peer</value>
        /// <example>192.168.0.1</example>
        [Display(Name = "IPv4 Peer Address")]
        public string Ipv4PeerAddress { get; private set; }

        /// <summary>
        /// The name of the attachment set to which the tenant IP network is associated.
        /// </summary>
        /// <value>A string value denoting the name of the attachment set</value>
        /// <example>713faafc85ff43db8472b6b9c38033a1</example>
        [Display(Name = "Attachment Set Name")]
        public string AttachmentSetName { get; private set; }

        /// <summary>
        /// The local IP routing preference applied to the route towards the tenant IP network
        /// </summary>
        /// <value>Integer representing the local IP routing preference</value>
        /// <example>200</example>
        [Display(Name = "Local IP Routing Preference")]
        public int? LocalIpRoutingPreference { get; private set; }
    }
}
