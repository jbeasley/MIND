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
    /// Model for requesting a new vpn attachment set (i.e. an attachment set association with a vpn)
    /// </summary>
    public class VpnAttachmentSetRequestViewModel
    {
        /// <summary>
        /// The topology type of the VPN for which the attachment set is to be associated.
        /// </summary>
        /// <value>Enum of type TopologyTypeEnum denoting the topology type of the vpn</value>
        public TopologyTypeEnum VpnTopologyType { get; set; }

        /// <summary>
        /// /The name of the tenant owner of the attachment set
        /// </summary>
        /// <value>String value denoting the name of the tenant</value>
        /// <example>Elektron</example>
        [Display(Name = "Tenant Name")]
        public string TenantName { get; set; }

        /// <summary>
        /// The name of the attachment set
        /// </summary>
        /// <value>A string denoting the name of the attachment set</value>
        /// <example>db7c48eaa9864cd0b3aa6af08c8370d6</example>
        [Display(Name = "Attachment Set Name")]
        public string AttachmentSetName { get; set; }

        /// <summary>
        /// The name of the region to which the attachment set belongs
        /// </summary>
        /// <value>A string denoting the name of the region</value>
        /// <example>EMEA</example>
        [Display(Name = "Region")]
        public string Region { get; set; }

        /// <summary>
        /// The redundancy setting configured for the attachment set
        /// </summary>
        /// <value>A string denoting the redundancy setting</value>
        /// <example>Bronze</example>
        [Display(Name = "Attachment Redundancy")]
        public string AttachmentRedundancy { get; set; }

        /// <summary>
        /// Determines if the attachment set should be configured as a hub for the association with the vpn.
        /// The vpn topology must be 'hub-and-spoke' for the attachment set to be defined as a hub.
        /// </summary>
        /// <value>Boolean value denoting the hub state of the attachment set</value>
        /// <example>true</example>
        [Display(Name="Hub")]
        public bool IsHub { get; set; }

        /// <summary>
        /// Determines if the attachment set should be directly integrated with the tenant multicast domain.
        /// The vpn must be enabled for multicast for the attachment set to be integrated with the tenant multicast domain.
        /// </summary>
        /// <value>Boolean value denoting whether the attachment set should be directly integrated with the tenant multicast domain</value>
        /// <example>true</example>
        [Display(Name = "Multicast Directly-Integrated")]
        public bool IsMulticastDirectlyIntegrated { get; set; }
    }
}
