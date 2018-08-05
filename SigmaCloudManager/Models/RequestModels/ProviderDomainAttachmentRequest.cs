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

namespace SCM.Models.RequestModels
{ 
    /// <summary>
    /// Model for requesting tenant attachment to the provider domain
    public partial class ProviderDomainAttachmentRequest
    { 
        /// <summary>
        /// Determines if the attachment is enabled for layer 3
        /// </summary>
        /// <value>Determines if the attachment is enabled for layer 3</value>
        public bool? IsLayer3 { get; set; }

        /// <summary>
        /// Determines if a bundle style of attachment is required
        /// </summary>
        /// <value>Determines if a bundle style of attachment is required</value>
        public bool? BundleRequired { get; set; }

        /// <summary>
        /// Determines if a multi port style of attachment is required
        /// </summary>
        /// <value>Determines if a multi port style of attachment is required</value>
        public bool? MultiportRequired { get; set; }

        /// <summary>
        /// Determines if the attachment should be enabled for tagging
        /// </summary>
        /// <value>Determines if the attachment should be enabled for tagging</value>
   
        public bool? IsTagged { get; set; }

        /// <summary>
        /// The name of a provider network location within which the new attachment will be provisioned
        /// </summary>
        /// <value>The name of a provider network location within which the new attachment will be provisioned</value>
        [Required]
        public string LocationName { get; set; }

        /// <summary>
        /// The name of a port pool which ports for the new attachment will be allocated from
        /// </summary>
        /// <value>The name of a port pool</value>
        [Required]
        public string PortPoolName { get; set; }

        /// <summary>
        /// The name of an attachment role which sets certain constrains on how the attachment must be configuted
        /// </summary>
        /// <value>The name of an attachment role</value>
        [Required]
        public string AttachmentRoleName { get; set; }

        /// <summary>
        /// Optional name of the provider network plane within which the attachment will be provisioned
        /// </summary>
        /// <value>Optional name of the provider network plane within which the attachment will be provisioned</value>
        public enum PlaneNameEnum
        { 
            /// <summary>
            /// Enum RedEnum for Red
            /// </summary>
            RedEnum = 1,
            
            /// <summary>
            /// Enum BlueEnum for Blue
            /// </summary>
            BlueEnum = 2
        }

        /// <summary>
        /// Optional name of the provider network plane within which the attachment will be provisioned
        /// </summary>
        /// <value>Optional name of the provider network plane within which the attachment will be provisioned</value>
        public PlaneNameEnum? PlaneName { get; set; }

        /// <summary>
        /// The required bandwidth of the attachment in Gbps
        /// </summary>
        /// <value>The required bandwidth of the attachment in Gbps</value>
        public int? AttachmentBandwidthGbps { get; set; }

        /// <summary>
        /// The required contract bandwidth in Mbps
        /// </summary>
        /// <value>The required contract bandwidth in Mbps</value>
        public int? ContractBandwidthMbps { get; set; }

        /// <summary>
        /// Determines whether DSCP and COS markings of packets sent from the tenant network should be trusted by the provider
        /// </summary>
        /// <value>Determines whether DSCP and COS markings of packets sent from the tenant network should be trusted by the provider</value>
        public bool? TrustReceivedCosDscp { get; set; }

        /// <summary>
        /// IPv4 address assigned to the first connection in the attachment
        /// </summary>
        /// <value>IPv4 address assigned to the first connection in the attachment</value>
        public string IpAddress1 { get; set; }

        /// <summary>
        /// IPv4 subnet mask assigned to the first connection in the attachment
        /// </summary>
        /// <value>IPv4 subnet mask assigned to the first connection in the attachment</value>
        public string SubnetMask1 { get; set; }

        /// <summary>
        /// IPv4 address assigned to the second connection in the attachment
        /// </summary>
        /// <value>IPv4 address assigned to the second connection in the attachment</value>
        public string IpAddress2 { get; set; }

        /// <summary>
        /// IPv4 subnet mask assigned to the second connection in the attachment
        /// </summary>
        /// <value>IPv4 subnet mask assigned to the second connection in the attachment</value>
        public string SubnetMask2 { get; set; }

        /// <summary>
        /// IPv4 address assigned to the third connection in the attachment
        /// </summary>
        /// <value>IPv4 address assigned to the third connection in the attachment</value>
        public string IpAddress3 { get; set; }

        /// <summary>
        /// IPv4 subnet mask assigned to the third connection in the attachment
        /// </summary>
        /// <value>IPv4 subnet mask assigned to the third connection in the attachment</value>
        public string SubnetMask3 { get; set; }

        /// <summary>
        /// IPv4 address assigned to the fourth connection in the attachment
        /// </summary>
        /// <value>IPv4 address assigned to the fourth connection in the attachment</value>
        public string IpAddress4 { get; set; }

        /// <summary>
        /// IPv4 subnet mask assigned to the fourth connection in the attachment
        /// </summary>
        /// <value>IPv4 subnet mask assigned to the fourth connection in the attachment</value>
        public string SubnetMask4 { get; set; }

       
    }
}
