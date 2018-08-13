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

    }
}
