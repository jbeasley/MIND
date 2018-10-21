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
    /// Model for requesting an attachment within the tenant domain
    /// </summary>
    public partial class TenantDomainAttachmentRequest
    { 
        /// <summary>
        /// Determines if a bundle style of attachment is required
        /// </summary>
        /// <value>Boolean value which denoted whether a bundle style of attachment is required</value>
        public bool? BundleRequired { get; set; }

        /// <summary>
        /// The minimum number of active links in a bundle attachment
        /// </summary>
        /// <value>Integer value which specifies the minimum links in the bundle</value>
        public int? BundleMinLinks { get; set; }

        /// <summary>
        /// The maximum number of active links in a bundle attachment
        /// </summary>
        /// <value>Integer value which specifies the maximum links in the bundle</value>
        public int? BundleMaxLinks { get; set; }

        /// <summary>
        /// Determines if a multi port style of attachment is required
        /// </summary>
        /// <value>Boolean value which Determines if a multi port style of attachment is required</value>
        public bool? MultiportRequired { get; set; }

        /// <summary>
        /// The name of a port pool from which ports for the new attachment will be allocated from
        /// </summary>
        /// <value>String value denoting the name of a port pool</value>
        public string PortPoolName { get; set; }

        /// <summary>
        /// The name of an attachment role which sets certain constrains on how the attachment must be configuted
        /// </summary>
        /// <value>String value denoting the name of an attachment role</value>
        public string AttachmentRoleName { get; set; }

        /// <summary>
        /// The required bandwidth of the attachment in Gbps
        /// </summary>
        /// <value>Integer value denoting the required bandwidth of the attachment in Gbps</value>
        public int? AttachmentBandwidthGbps { get; set; }

        /// <summary>
        /// The required contract bandwidth in Mbps
        /// </summary>
        /// <value>Integer value denoting the required contract bandwidth in Mbps</value>
        public int? ContractBandwidthMbps { get; set; }

        /// <summary>
        /// Determines whether DSCP and COS markings of packets sent from the provider network should be trusted by the tenant
        /// </summary>
        /// <value>Boolean value which denotes whether the DSCP and COS markings should be trusted</value>
        public bool? TrustReceivedCosAndDscp { get; set; }

        /// <summary>
        /// A description for the new attachment
        /// </summary>
        /// <value>String value for the description</value>
        public string Description { get; set; }

        /// <summary>
        /// Notes for the new attachment
        /// </summary>
        /// <value>String value for notes</value>
        public string Notes { get; set; }

        /// <summary>
        /// IPv4 addresses to be assigned to the interfaces of the attachment.
        /// </summary>
        /// <value>A list of IPv4 addresses and subnet masks</value>
        public List<Ipv4AddressAndMask> Ipv4Addresses { get; set; }
    }
}
