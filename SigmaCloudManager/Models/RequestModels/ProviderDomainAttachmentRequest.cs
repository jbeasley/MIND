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
using Mind.Models.RequestModels;

namespace SCM.Models.RequestModels
{ 
    /// <summary>
    /// Model for requesting tenant attachment to the provider domain
    /// </summary>
    public partial class ProviderDomainAttachmentRequest
    { 
        /// <summary>
        /// Determines if a bundle style of attachment is required
        /// </summary>
        /// <value>Determines if a bundle style of attachment is required</value>
        public bool? BundleRequired { get; set; }

        /// <summary>
        /// The minimum number of active links in a bundle attachment
        /// </summary>
        /// <value>A value which specifies the minimum links in the bundle</value>
        public int? BundleMinLinks { get; set; }

        /// <summary>
        /// The maximum number of active links in a bundle attachment
        /// </summary>
        /// <value>A value which specifies the maximum links in the bundle</value>
        public int? BundleMaxLinks { get; set; }

        /// <summary>
        /// Determines if a multi port style of attachment is required
        /// </summary>
        /// <value>Determines if a multi port style of attachment is required</value>
        public bool? MultiportRequired { get; set; }

        /// <summary>
        /// The name of a provider network location within which the new attachment will be provisioned
        /// </summary>
        /// <value>The name of a provider network location within which the new attachment will be provisioned</value>
        public string LocationName { get; set; }

        /// <summary>
        /// The name of a port pool which ports for the new attachment will be allocated from
        /// </summary>
        /// <value>The name of a port pool</value>
        public string PortPoolName { get; set; }

        /// <summary>
        /// The name of an attachment role which sets certain constrains on how the attachment must be configuted
        /// </summary>
        /// <value>The name of an attachment role</value>
        public string AttachmentRoleName { get; set; }

        /// <summary>
        /// Optional name of the provider network plane within which the attachment will be provisioned
        /// </summary>
        /// <value>Optional name of the provider network plane within which the attachment will be provisioned</value>
        public PlaneEnum? PlaneName { get; set; }

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
        public bool? TrustReceivedCosAndDscp { get; set; }


        /// <summary>
        /// IPv4 addresses to be assigned to the interfaces of the attachment.
        /// </summary>
        /// <value>A list of IPv4 addresses and subnet masks</value>
        public List<Ipv4AddressAndMask> Ipv4Addresses { get; set; }
    }
}
