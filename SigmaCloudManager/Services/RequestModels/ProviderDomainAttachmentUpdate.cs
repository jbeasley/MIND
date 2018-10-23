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
    /// Model for updating an existing tenant attachment to the provider domain
    /// </summary>
    public partial class ProviderDomainAttachmentUpdate
    { 
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
        /// Determines if the updated attachment should be associated with an existing routing instance
        /// </summary>
        public string ExistingRoutingInstanceName { get; set; }

        /// <summary>
        /// Determines if the updated attachment should be associated with a new routing instance.
        /// </summary>
        public bool? CreateNewRoutingInstance { get; set; }

        /// <summary>
        /// Determines if the updated attachment should use jumbo MTU
        /// </summary>
        public bool? UseJumboMtu { get; set; }

        /// <summary>
        /// The minimum number of active links in the updated bundle attachment
        /// </summary>
        /// <value>A value which specifies the minimum links in the bundle</value>
        public int? BundleMinLinks { get; set; }

        /// <summary>
        /// The maximum number of active links in the updated bundle attachment
        /// </summary>
        /// <value>A value which specifies the maximum links in the bundle</value>
        public int? BundleMaxLinks { get; set; }

        /// <summary>
        /// A description for the attachment
        /// </summary>
        /// <value>String value for the description</value>
        public string Description { get; set; }

        /// <summary>
        /// Notes for the attachment
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
